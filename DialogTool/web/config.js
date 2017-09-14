angular.module('config', [])
    .factory('config', function(){
        return {
            websocketPath: 'ws://localhost:5555/state',
            websocketReconnectTime: 2000, // every two seconds
        }
    })