module.exports = function(grunt) {

    var buildPath = grunt.option('output') || './build/';


	grunt.initConfig({
		less: {
			build: {
				options: {
					compress: true,
					yuicompress: true,
					optimization: 2,
					paths: ['.'],
				},
				files: {
					"./app/all.css": "./app/**/*.less" // destination file and source file
				}
			},
		},
        copy: {
            build: {
                files: [
					{
						expand: true,
						src: [
                            './index.html',
                            './config.js'
                        ],
						dest: buildPath
					},
                    {
                        expand: true, 
                        src: ['./app/**'],
						dest: buildPath
                    }, 
					{
						expand: true,
						src: [
                            './node_modules/angular/angular.min.js',
                            './node_modules/angular-animate/angular-animate.min.js',
                            './node_modules/angular-aria/angular-aria.min.js',
                            './node_modules/angular-material/angular-material.min.js',
                            './node_modules/angular-material/angular-material.css',
                        ],
						dest: buildPath
					}
                ]
            }
        },
        clean: {
            build: [buildPath]
        },
		watch: {
			dev: {
				reload: true,
				files: ['./app/**/*'],
				tasks: ['less']
			}
		}, 
		// open: {
		// 	extension: {
		// 		path: 'http://reload.extensions/',
		// 		app: 'Google Chrome'
		// 	}
		// }
	});

	grunt.loadNpmTasks('grunt-contrib-less');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-contrib-clean');
	grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('build', ['less:build', 'clean:build', 'copy:build'])
	grunt.registerTask('dev', ['watch:dev'])
};