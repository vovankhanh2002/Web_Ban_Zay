$(document).ready(function () {
    ShowCart();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var size = 36;
        var tsize = $('product-size').val();
        var tquatity = $('#product-quanity').val();
        if (tquatity != null && tsize != null) {
            quatity = parseInt(tquatity);
            size = parseInt(tsize);
        }

        $.ajax({
            url: '/Cart/Addtocart',
            type: 'POST',
            data: { id: id, quantity: quatity, size: size },
            success: function (rs) {
                if (rs.Sucsess) {
                    $('#cart_span_nav').html(rs.count);
                    alert(rs.msg);
                }
            }
        });
    });
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có muốn xóa tất cả sản phẩm khỏi giỏ hàng')
        if (conf == true) {
            DeleteAllCart();
        }

    });
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        var size = $('#Size_' + id).val();
        UpdateCart(id, quantity, size);

    });
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có muốn xóa sản phẩm khỏi giỏ hàng')
        if (conf == true) {
            $.ajax({
                url: '/Cart/DeleteCart',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Sucsess) {
                        $('#cart_span_nav').html(rs.count);
                        $('#trow_' + id).remove()
                        LoadCart();
                    }
                }
            });
        }
       
    });

});

function DeleteAllCart() {
    $.ajax({
        url: '/Cart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Sucsess) {
                LoadCart()
            }
            
        }
    });
}

function UpdateCart(id,quantity,size) {
    $.ajax({
        url: '/Cart/UpdateCart',
        type: 'POST',
        data: { id: id, quantity: quantity, size: size },
        success: function (rs) {
            if (rs.Sucsess) {
                LoadCart()
            }

        }
    });
}

function ShowCart() {
    $.ajax({
        url: '/Cart/ShowCart',
        type: 'GET',
        success: function (rs) {
            $('#cart_span_nav').html(rs.count);
        }
    });
}
function LoadCart() {
    $.ajax({
        url: '/Cart/PartialCart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}