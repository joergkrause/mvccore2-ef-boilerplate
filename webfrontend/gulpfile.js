const gulp = require('gulp');

gulp.task('copy', function() {
    // Glob ** *. !*.*test*.
    return gulp.src('./node_modules/bootstrap/dist/css/bootstrap.css')
            .pipe(gulp.dest('./wwwroot/css'));

});

gulp.task('default', ['copy']);