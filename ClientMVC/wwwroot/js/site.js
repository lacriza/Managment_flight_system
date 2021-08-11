// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function populateStorage() {
  localStorage.setItem('priceFrom', '0');
  localStorage.setItem('priceTo', '500');
}
if (localStorage.getItem('priceTo') === undefined || localStorage.getItem('priceTo') == undefined) {
  populateStorage();
}
