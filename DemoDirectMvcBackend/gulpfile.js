/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

//gulp.task('default', function () {
//    console.log("Hello Gulp");
//});

var concat = require('gulp-concat');
gulp.task('concat-scripts', function () {
    return gulp.src([
        'Content/rsolap/RadarSoft.RadarCube.jquery.js',
        'Content/rsolap/spectrum.js',
        'Content/rsolap/jqPropertyGrid.js',
        'Content/rsolap/jquery.treegrid.js',
        'Content/rsolap/RadarSoft.RadarCube.jquery.treeview.js',
        'Content/rsolap/RadarSoft.RadarCube.jquery-ui.js',
        'Content/rsolap/rsMvcOlapAnalysis.js'])
      .pipe(concat('rsMvcOlapAnalysis.all.js'))
      .pipe(gulp.dest('Content/rsolap'));
});

var minify = require('gulp-minify');
gulp.task('compress-scripts', function () {
    gulp.src('Content/rsolap/rsMvcOlapAnalysis.all.js')
        .pipe(minify({
            ext: {
                min: '.min.js'
            }
        }))
        .pipe(gulp.dest("Content/js"));
});

gulp.task('concat-styles', function () {
    return gulp.src([
        'Content/rsolap/css/RadarSoft.RadarCube.jquery.treeview.css',
        'Content/rsolap/css/RadarSoft.RadarCube.jquery-ui.css',
        'Content/rsolap/css/jquery.treegrid.css',
        'Content/rsolap/css/spectrum.css',
        'Content/rsolap/css/jqPropertyGrid.css'])
      .pipe(concat('rsMvcOlapAnalysis.all.min.css'))
      .pipe(gulp.dest('Content/rsolap/css'));
});


var cleanCSS = require('gulp-clean-css');
gulp.task('compress-styles', function () {
    return gulp.src('Content/rsolap/css/rsMvcOlapAnalysis.all.min.css')
        .pipe(cleanCSS({ debug: true }, function (details) {
            console.log(details.name + ': ' + details.stats.originalSize);
            console.log(details.name + ': ' + details.stats.minifiedSize);
        }))
        .pipe(gulp.dest('Content/css'));
});

