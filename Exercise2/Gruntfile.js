/// <binding BeforeBuild='all' ProjectOpened='watch' />
module.exports = function (grunt) {
    grunt.initConfig({
        clean: {
            all: ["wwwroot/css/*", "wwwroot/js/*", "temp/*"]
        },
        concat: {
            all: {
                src: ['src/js/site.js', 'node_modules/bootstrap/dist/js/bootstrap.bundle.js'],
                dest: 'temp/js/site.js'
            }
        },
        sass: {
            all: {
                files: {
                    'temp/css/site.css': 'src/scss/site.scss'
                }
            }
        },
        cssmin: {
            all: {
                files: {
                    'wwwroot/css/site.min.css': "temp/css/site.css"
                }
            }
        },
        uglify: {
            all: {
                src: ['temp/js/site.js'],
                dest: 'wwwroot/js/site.min.js'
            }
        },
        watch: {
            files: ["src/**/*"],
            tasks: ["all"]
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask("all", ['clean', 'concat', 'sass', 'cssmin', 'uglify']);
};