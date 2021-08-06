$(function () {
  $("#slider-range").slider({
    range: true,
    min: 0,
    max: 1000,
    values: [localStorage.getItem('priceFrom'), localStorage.getItem('priceTo')],
    slide: function (event, ui) {
      var to = ui.values[1];
      var from = ui.values[0];
      $("#amount").val(from + "-" + to);
      localStorage.setItem('priceFrom', from);
      localStorage.setItem('priceTo', to);
    }
  });
  $("#amount").val($("#slider-range").slider("values", 0) + "-"
    + $("#slider-range").slider("values", 1));
});

$('#myModal').on('shown.bs.modal', function () {
  $('#myInput').trigger('focus')
})