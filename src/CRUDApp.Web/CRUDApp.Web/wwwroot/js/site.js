﻿$(document).ready(() => {

    const ClearElems = () => {
        $(".editOrderItem").remove();
        $(".detailOrderItem").remove();
    }

    const UpdateOrderItemListener = () => {
        //item edit save
        $(".editOrderItem").on("submit", (e) => {
            e.preventDefault();

            let model = {
                name: e.target.getElementsByTagName("input")[0].value,
                unit: e.target.getElementsByTagName("input")[1].value,
                quantity: e.target.getElementsByTagName("input")[2].value,
                orderId: e.target.dataset.order,
                itemId: e.target.dataset.item,
            }

            $.post("/order/edititem", { model }, resp => {
                if (resp != false) {
                    e.target.getElementsByTagName("button")[1].textContent = "Сохранено успешно";
                    setTimeout(() => {
                        e.target.getElementsByTagName("button")[1].textContent = "Сохранить";
                    }
                        , 2000);

                    //$("#orderItemsLoad").append()
                }
                else {
                    e.target.getElementsByTagName("button")[1].textContent = "Ошибка! Проверьте ввод";
                    setTimeout(() => {
                        e.target.getElementsByTagName("button")[1].textContent = "Сохранить";
                    }
                        , 2000);
                }
            })

        })
    }

    const RemoveOrderItemListener = () => {
        //delete item
        $(".btnRemoveOrderItem").on("click", (e) => {
            e.preventDefault();
            let model = {
                orderItemId: e.target.dataset.item
            }

            $.post("/order/deleteitem", { model }, resp => {
                if (resp != false) {
                    e.target.closest("form").remove();
                } else alert("При удалении возникли ошибки");
            })
        })
    }

    //add el to order
    $("#addOrderElemForm").on("submit", (e) => {
        e.preventDefault();

        let model = {
            name: e.target.getElementsByTagName("input")[0].value,
            quantity: e.target.getElementsByTagName("input")[1].value,
            unit: e.target.getElementsByTagName("input")[2].value,
            orderId: $("#editOrderId").val()
        }

        $.post("/order/additem", { model }, resp => {
            if (resp != false) {
                alert("Добавлен успешно");
                location.reload();
            }
            else alert("При создании что-то пошло не так, проверьте ввод");
        })
    });

    //edit Order
    $("#editOrderSave").on("click", (e) => {
        e.preventDefault();

        let model = {
            number: $("#editOrderNumber").val(),
            date: $("#editOrderDate").val(),
            providerId: $("#editOrderProviderSelect").val(),
            orderId: $("#editOrderId").val()
        }

        $.post("/order/update", { model }, resp => {
            if (resp != null) {
                alert("Сохранено");
            }
            else alert("При сохранении что-то пошло не так, проверьте ввод");
        })

    })

    //create order
    $("#createWindow").on("submit", (e) => {
        e.preventDefault();

        let model = {
            number: e.target.getElementsByTagName("input")[0].value,
            providerId: e.target.getElementsByTagName("select")[0].value,
            date: e.target.getElementsByTagName("input")[1].value,
        }

        console.log(model);

        $.post("/order/create", { model }, (resp) => {
            if (resp == null) {
                alert("При создании возникли ошибки, проверьте ввод");
            }
            else {
                alert("Новый заказ успешно создан!");
            }
        })
    });

    // close create
    $("#btnCreateClose").on("click", () => {
        $("#hideZoom").addClass("d-none");
        $("#createWin").addClass("d-none");
    });

    // show create
    $("#showCreateWin").on("click", () => {
        $("#hideZoom").removeClass("d-none");
        $("#createWin").removeClass("d-none");
    });

    //delete order
    $("#deleteOrderBtn").on("click", (e) => {
        e.preventDefault();
        let model = {
            orderId: e.target.dataset.order
        }
        $.post("/order/delete", { model }, resp => {
            if (resp != false) {
                alert("Удаление прошло успешно");
            }
            else alert("При удалении возникли ошибки");
        })
    });

    // close edit
    $("#btnEditClose").on("click", (e) => {
        ClearElems();
        $("#hideZoom").addClass("d-none");
        $("#editWin").addClass("d-none");
    })

    // show edit
    $("#showEditWindow").on("click", () => {
        $("#hideZoom").removeClass("d-none");
        $("#editWin").removeClass("d-none");
        $("#detailWin").addClass("d-none");
    });

    //show detail
    $(".showOrderDetail").on("click", (e) => {
        let id = e.target.dataset.id;
        ClearElems();
        $.get("/order/detail", { id }, resp => {
            console.log(resp);

            if (resp != null) {
                $("#deleteOrderBtn").attr("data-order", resp.id);

                $("#orderDetailNumber").text(resp.number);
                $("#orderDetailDate").text(resp.date);
                $("#orderDetailProvider").text(resp.provider.name);

                //editOrderProvider
                $("#editOrderNumber").val(resp.number)
                $("#editOrderDate").val(resp.date)
                $("#editOrderId").val(resp.id);
                $(`.editOrderProvider[data-providerId="${resp.provider.id}"]`).attr("selected", true);

                resp.items.forEach(x => {
                    $("#orderItemsLoad").append(`<form data-item="${x.id}" data-order="${resp.id}" class="editOrderItem border col-12 bg-light"><div class="d-flex align-items-center"><label>Name</label><input type="text" class="ms-2 form-control-sm form-control br-md" placeholder="${x.name}"/></div><div class="mt-1 d-flex align-items-center"><label>Unit</label><input type="text" class="ms-2 form-control-sm form-control br-md" placeholder="${x.unit}"/></div><div class="mt-1 d-flex align-items-center"><label>Quantity</label><input type="number" min="0" step="0.001" class="ms-2 form-control-sm form-control br-md" placeholder="${x.quantity}"/></div><div><button data-item="${x.id}" class="btnRemoveOrderItem text-danger btn bg-white">Удалить</button><button type="submit" class="btn bg-white">Сохранить</button></div></form>`);
                })

                resp.items.forEach(x => {
                    $("#orderDetailItems").append(`<div class="detailOrderItem br-md mb-2 border p-2 col-12 text-start bg-light rd-md"><div><label>Unit: ${x.unit}</label></div><div><label>Quantity: ${x.quantity}</label></div><div><label>Name: ${x.name}</label></div></div>`);
                })

                UpdateOrderItemListener();
                RemoveOrderItemListener();

                $("#hideZoom").removeClass("d-none");
                $("#detailWin").removeClass("d-none");

            }
        });
    });

    //close detail
    $("#btnDetailClose").on("click", () => {
        $("#hideZoom").addClass("d-none");
        $("#detailWin").addClass("d-none");
    });

    //close edit
    $("#btnEditClose").on("click", () => {
        ClearElems();
        $("#hideZoom").addClass("d-none");
        $("#editWin").addClass("d-none");
    });

    $("#addOrderElemClose").on("click", (e) => {
        $("#addOrderElemForm").addClass("d-none");
    });

    $("#addOrderElemShow").on("click", (e) => {
        $("#addOrderElemForm").removeClass("d-none");
    });
});