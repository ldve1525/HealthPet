var d = document.getElementById("date");
var today = d.value;

if (today != "") {
    today = new Date(today);
    today.setHours(today.getHours() + 6);

    var shift1 = document.getElementById("shift");

    
}
else {
    var date = Date.now();
    today = new Date(date);

    var shifts = (2 * (today.getHours() - 8)) + ~~(today.getMinutes() / 30) + 1;

    today.setHours(today.getHours() - 6);

    document.getElementById('date').valueAsDate = today;

    const $select = document.querySelector('#shift');
    alert($select.value);
    //$select.value = "0";
}

var formattedDate = today.toISOString();

myFunction(formattedDate);

function myFunction(val) {
    $.ajax({
        url: '/Appointments/GetShifts/',
        type: 'get',
        data: { date: val },
        success: function (result) {
            $("#shift option").attr('disabled', false).css("background-color", "white").css("display", "block");

            var tomorrow = new Date();
            tomorrow.setDate(today.getDate() + 1);
            tomorrow.setHours(0, 0, 0, 0);

            var selected = new Date(val);
            selected.setHours(selected.getHours() + 6);
            //alert("selected: " + selected + " tomorrow: " + tomorrow);

            if (selected < tomorrow) {
                for (var i = 1; i <= shifts; i++) {
                    $("#shift option[value='" + i + "']").attr('disabled', true).css("display", "none");
                }
            }

            var list = result.list;
            var shift = document.getElementById("shift");

            $.each(list, function (index, value) {
                if (value != shift.value) {
                    $("#shift option[value='" + value + "']").attr('disabled', true).css("background-color", "lightgray");
                }
            });

            $(shift).prop('disabled', false);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
            alert(err);
        }
    });
}

function sendEmail(val) {
    $.ajax({
        url: '/Appointments/Print/',
        type: 'get',
        data: { id: val },
        success: function (result) {
            $("#content").html("El correo electrónico ha sido enviado.");
            $("#btnClose").css("display", "block");
            $("#btnSend").css("display", "none");
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
            alert(err);
        }
    });
}

function confirmEdit() {
    var modal = $("#editModal");
    modal.modal("hide");
    $("#confirmEdit").click();  //activa el event handler de clic del envío real
}

function confirmDelete() {
    alert("confirmDelete");
    var modal = $("#deleteModal");
    modal.modal("hide");
    $("#confirmDelete")[0].click();  //activa el event handler de clic del envío real
}