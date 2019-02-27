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
var loading = false;

function loadPage(page, title){
  if(loading == false) {
    loading = true;
    $("main").load(page, function () {
      document.title = documentTitle + " - " + title;
      M.AutoInit();
      loading = false;
    });
  }
}

/* SIDENAV */

$('a[href="#trading"]').click(function(){
  loadPage("/trading", "Trading");
});

$('a[href="#achievements"]').click(function(){
  loadPage("/achievements", "Achievements");
});

$('a[href="#settings"]').click(function(){
  loadPage("/settings", "Settings");
});

$('a[href="#items"]').click(function(){
  loadPage("/items", "Items");
});
