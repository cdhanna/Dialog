var app = angular.module( 'dialog', [ 'config', 'ngMaterial' ] )

    .config([
        '$mdThemingProvider', 
        function($mdThemingProvider){
            $mdThemingProvider.theme('dark-orange').backgroundPalette('orange').dark();
        }
    ])

    .controller("mainCtrl", [
        //deps 
        'websocketSrvc',
        //code
        function(websocketSrvc){

            var count = 0;
            var cb = websocketSrvc.onMessage('tuna', function(data){
                count += 1;
                console.log('got data', data);

                if (count > 5){
                    cb.cancel();
                }
            });
            


            websocketSrvc.send('state', {
                fetch: true
            }).then(function(){
                console.log('message sent')
            }); 

        }
    ])