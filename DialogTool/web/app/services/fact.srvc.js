app.service('factSrvc', [
    // deps 
    'websocketSrvc', 
    '$timeout',
    // code 
    function(websocketSrvc, $timeout){
        var self = {};

        var facts = [];
        var factLookup = {
            // key -> index
        };

        websocketSrvc.onMessage('facts', function(data){
            $timeout(function(){
                data.facts.forEach(function(fact){
                    fact.key = fact.key.toLowerCase();
                    var index = factLookup[fact.key];
                    if (index == undefined){
                        facts.push({
                            key: fact.key,
                            value: fact.value
                        });
                        factLookup[fact.key] = facts.length - 1;
                    } else {
                        facts[index].value = fact.value;
                    }

                })  
            })

        });

        self.getFacts = function(){
            return facts;
        };
        self.getFact = function(key){
            return factLookup[key];
        };

        return self;
    }
])