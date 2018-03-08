const gulp = require('gulp');

gulp.task('copy:css', function() {
    // Glob ** *. !*.*test*.
    return gulp.src('./node_modules/bootstrap/dist/css/bootstrap.css')
            .pipe(gulp.dest('./wwwroot/css'));

});

gulp.task('copy:js', function() {
    // Glob ** *. !*.*test*.
    return gulp.src([
        './node_modules/bootstrap/dist/js/bootstrap.css',
        './node_modules/jquery/dist/jquery.js',
        './node_modules/bootstrap/dist/js/bootstrap.js',
        './node_modules/bootstrap/dist/js/bootstrap.css',
    ]).pipe(gulp.dest('./wwwroot/css'));

});

gulp.task('default', ['copy:css', 'copy:js']);