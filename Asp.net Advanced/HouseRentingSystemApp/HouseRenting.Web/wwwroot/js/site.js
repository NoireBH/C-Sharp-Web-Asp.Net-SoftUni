function statistics() {
    $('#statistics-btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($('#statistics').hasClass('d-none')) {
            $.get('https://localhost:7068/api/statistics', function (data) {
                $('#total-houses').text(data.totalHouses + " Houses");
                $('#total-rents').text(data.totalRents + " Rents");
                $('#statistics').removeClass('d-none');

                $('#statistics-btn').text('Hide Statistics');
                $('#statistics-btn').removeClass('btn-primary');
                $('#statistics-btn').addClass('btn-danger');
            });
        }
        else {
            $('#statistics').addClass('d-none');

            $('#statistics-btn').text('Show Statistics');
            $('#statistics-btn').removeClass('btn-danger');
            $('#statistics-btn').addClass('btn-primary');
        }

    });
}
