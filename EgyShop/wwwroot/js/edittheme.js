//Product hover setting
$(".card").hover(
    function () {
        $(this).addClass('shadow-lg').addClass('bg-white').addClass('rounded');

    },
    function () {
        $(this).removeClass('hover');
        $(this).removeClass('shadow-lg').removeClass('bg-white').removeClass('rounded');
    }
);

//open sideNav bar setting   
$(".right-button").click(function () {
    document.getElementById("mySidenav").style.width = "250px";
    $(".right-button").hide();
})
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    $(".right-button").show();

}
// change navbar setting 
$("#favcolor").on("change", function () {
    $(".navbar").css("background-color", $(this).val());
});
$("#navTextcolor").on("change", function () {
    $(".navbar li").css("color", $(this).val());
    $(".navbar li a").css("color", $(this).val());
});
$("#navbarBrandTextColor").on("change", function () {
    $(".navbar-brand").css("color", $(this).val());
});

//change website dexription section
$("#StoreDescriptionSection").on("change", function () {
    if ($(this).is(":checked")) {
        $(".image-container ").css("display", "block");
    } else {

        $(".image-container ").css("display", "none");

    }
});
$("#storeDescriptionColor").on("change", function () {
    $(".image-container P").css("color", $(this).val());
});

//change Feature Products Setting
$("#featureProducts").on("change", function () {
    if ($(this).is(":checked")) {
        $(".featured-prouducts").css("display", "block");
    } else {

        $(".featured-prouducts").css("display", "none");
    }
});
//change recommend Products Setting
$("#recommendProducts").on("change", function () {
    if ($(this).is(":checked")) {
        $(".recommend-products").css("display", "block");
    } else {

        $(".recommend-products").css("display", "none");
    }
});
//changeCategoryProductsSection
$("#StoreCategoriesProducts").on("change", function () {
    if ($(this).is(":checked")) {
        $(".category-products").css("display", "block");
    } else {

        $(".category-products").css("display", "none");
    }
});



//Change Products Row
$(".featureRowBtn").click(
    function () {
        $(".card-container").removeClass('col-md-' + x);
        var rowValue = $(this).val();
        x = rowValue;

        $("#numOfRows").val(rowValue);
        $(".card-container").addClass('col-md-' + rowValue);

    }
);


$("#ProductImageEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".ProductImage ").css("display", "block");
    } else {

        $(".ProductImage ").css("display", "none");

    }
});
$("#ProductNameEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".ProductName ").css("display", "block");
    } else {

        $(".ProductName ").css("display", "none");

    }
});


$("#ProductCategoryEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".productCategory").css("display", "block");
    } else {

        $(".productCategory").css("display", "none");

    }
});
$("#ProductDescriptioneEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".ProductDescription").css("display", "block");
    } else {

        $(".ProductDescription").css("display", "none");

    }
});
$("#ProductPriceEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".ProductPrice").css("display", "block");
    } else {

        $(".ProductPrice").css("display", "none");

    }
});
/*
$("#ProductCategoryEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $(".image-container ").css("display", "block");
    } else {

        $(".image-container ").css("display", "none");

    }
});
*/
$("#ProductButtonEdit").on("change", function () {
    if ($(this).is(":checked")) {
        $("#ProductButton").css("display", "block");
    } else {

        $("#ProductButton").css("display", "none");

    }
});

//change Footer Settings

$("#footerBackground").on("change", function () {

    $(".footer").css("background-color", $(this).val());

});
$("#footerTextcolor").on("change", function () {
    $(".footer").css("color", $(this).val());
    $(".footer-icons li a").css("color", $(this).val());
});
$("#footerIconcolor").on("change", function () {
    $(".footer ul li i").css("color", $(this).val());
});


$("#showContactInfoFooter").on("change", function () {
    if ($(this).is(":checked")) {
        $(".footer .footer-icons").css("visibility", "visible");
    } else {

        $(".footer .footer-icons").css("visibility", "hidden");
    }
});