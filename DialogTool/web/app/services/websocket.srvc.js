app.service('websocketSrvc', [
    // deps 
    'config', 
    'guidSrvc', 

    // code 
    function(config, guidSrvc){
        var self = {};

        var topics = {
            // name -> topic
        };
        var messageQueue = [];

        var ws = new WebSocket(config.websocketPath);
        attachWsListeners();

        setInterval(attemptConnection, config.websocketReconnectTime);

        function attachWsListeners(){
            ws.onopen = function(ctx){
                console.debug('ws open', ctx);

                emptyMessageQueue();
            };
            ws.onclose = function(ctx){
                console.debug('ws close', ctx);
            };
            ws.onerror = function(ctx){
                console.debug('ws error', ctx);
            };
            ws.onmessage = function(ctx){
                console.debug('ws message', ctx);

                var data = JSON.parse(ctx.data);
                var topic = topics[data.topic];
                if (topic){
                    console.debug('triggering ', topic.name, data)
                    topic.trigger(data);
                }
            };
        }

        self.onMessage = function(topic, callback){
            return addTopicCallback(topic, callback);
        };

        self.ping = function(topic, data){
            return attemptSend(topic, data);
        };
        
        self.send = function(topic, data){
            return new Promise(function(success){
                if (attemptSend(topic, data) == false){
                    messageQueue.push({
                        topic: topic, 
                        data: data, 
                        cb: success
                    });
                } else {
                    cb();
                }
            });
        };

        function attemptConnection(){
            if (ws.readyState == WebSocket.CLOSED){
                ws = new WebSocket(config.websocketPath);
                attachWsListeners();
            } else if (ws.readyState == WebSocket.OPEN){
                emptyMessageQueue();
            }
        }

        function emptyMessageQueue(){
            messageQueue = messageQueue.reduce(function(agg, msg){
                if ( attemptSend(msg.topic, msg.data) == false){
                    agg.push(msg);
                } else {
                    msg.cb();
                }
                return agg;
            }, [])
        }

        function attemptSend(topic, data){
            if (ws.readyState == WebSocket.OPEN){
                var toSend = {
                    topic: topic,
                    data: data
                };
                var toSendJson = JSON.stringify(toSend);
                ws.send(toSendJson);
                return true;
            } else {
                // console.error('Connection was not open. Sending Failed');
                return false;
            }
        }

        function addTopicCallback(topicName, callback){
            var topic = ensureTopic(topicName);
            var topicCallback = new TopicCallback(topic, callback);
            return topicCallback;
        };

        function ensureTopic(topicName){
            if (topics[topicName] == undefined) {
                topics[topicName] = new Topic(topicName);
            }
            return topics[topicName]
        }

        function TopicCallback(topic, callback){
            var t = this;

            t.topic = topic;
            t.callback = callback;
            t.id = guidSrvc.create();
            
            topic.callbacks[t.id] = this;

            t.cancel = function(){
                delete t.topic.callbacks[t.id];
            }
        };

        function Topic(topicName){
            var t = this;
            t.id = guidSrvc.create();
            t.name = topicName;
            t.callbacks = {
                // guid -> callback 
            };

            t.trigger = function(data){
                Object.keys(t.callbacks).forEach(function(id){
                    t.callbacks[id].callback(data);
                })
            }
            
        };

        return self;
    }
])