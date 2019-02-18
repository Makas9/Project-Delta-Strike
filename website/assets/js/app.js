/*
 * Welcome to your app's main JavaScript file!
 *
 * We recommend including the built version of this JavaScript file
 * (and its CSS file) in your base layout (base.html.twig).
 */

// any CSS you require will output into a single css file (app.css in this case)
require('../css/app.css');
const $ = require('./jquery.js');
global.$ = global.jQuery = $;
require('./materialize.min.js');

$(document).ready(function() {
   M.AutoInit();
});

/* LOADER */

var documentTitle = document.title;

function loadPage(page){
  $("main").load(page);
}

function prepareNewPage(title){
  document.title = documentTitle+" - "+title;
  setTimeout(function(){
    M.AutoInit();
  }, 1000);
}

/* SIDENAV */

$('a[href="#trading"]').click(function(){
  loadPage("/trading");
  prepareNewPage("Trading");
});

$('a[href="#achievements"]').click(function(){
  loadPage("/achievements");
  prepareNewPage("Achievements");
});

$('a[href="#settings"]').click(function(){
  loadPage("/settings");
  prepareNewPage("Settings");
});

$('a[href="#items"]').click(function(){
  loadPage("/items");
  prepareNewPage("Items");
});
