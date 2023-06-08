$(document).ready(function () {
    $('body').on('click','#btn_active', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: "/Admin/HoaDon/ChangeSatus",
            data: { id: id },
            dataType: "json",
            type: "POST",
            success: function (rs) {
                if (rs.duyet == 1) {
                    btn.text('Đã duyệt');
                    location.reload();
                } else {
                    btn.text('Chưa duyệt')
                    location.reload();
                }
            }
        })
    })
})




