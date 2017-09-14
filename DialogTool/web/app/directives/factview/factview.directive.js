app.directive('factView', [
    // deps 
    'factSrvc', 

    // code 
    function(factSrvc){
        return {
            templateUrl: './app/directives/factview/factview.directive.html',
            link: function(scope, elem, attrs){
                



                scope.query = '';
                scope.useRegex = false;
                scope.otherIndex = 0;
                scope.nonPinnedIndex = 0;

                scope.pinned = {
                    // key -> 
                }

                scope.updateQuery = function(){

                }

                // scope.$watch('')

                scope.getQueryFacts = function(){
                    // otherFacts.length = 0;
                    var otherFacts = [];
                    var queryFacts = [];
                    var pinnedQueryFacts = [];
                    var pinnedOtherFacts = [];
                    
                    var allFacts = [];

                    var q = scope.query.toLowerCase();
                    var comp = function(f){
                        return f.key.startsWith(q);
                    }
                    if (scope.useRegex == true)[
                        comp = function(f){
                             return new RegExp(scope.query).exec(f.key) != null;
                        }
                    ]

                    factSrvc.getFacts().forEach(function(fact){
                        var pass = comp(fact);
                        if (fact.pinned == true && pass == true){
                            pinnedQueryFacts.push(fact);
                        } else if (fact.pinned == true && pass == false){
                            pinnedOtherFacts.push(fact);
                        } else if (fact.pinned != true && pass == true){
                            queryFacts.push(fact);
                        } else if (fact.pinned != true && pass == false){
                            otherFacts.push(fact);
                        }

                    })
                    
                    pinnedQueryFacts.forEach(function(f){
                        allFacts.push(f);
                    })

                    pinnedOtherFacts.forEach(function(f){
                        allFacts.push(f);
                    });
                    scope.nonPinnedIndex = allFacts.length - 1;
                    
                    queryFacts.forEach(function(f){
                        allFacts.push(f);
                    })


                    scope.otherIndex = allFacts.length - 1;
                    otherFacts.forEach(function(f){
                        allFacts.push(f);
                    })

                   
                    return allFacts;

                    // if (scope.useRegex == true){
                    //     return factSrvc.getFacts().filter(function(fact){
                    //         return new RegExp(scope.query).exec(fact.key) != null;
                    //     });
                    // } else {
                    //     var q = scope.query.toLowerCase();
                    //     return factSrvc.getFacts().filter(function(fact){
                    //         return fact.key.startsWith(q);
                    //     });
                    // }
                }
            }
        }
    }
])