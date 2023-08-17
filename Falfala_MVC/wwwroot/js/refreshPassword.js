import { updateResultLabel } from "./miarTools.js";


$(function () {

    $("#btn_showPasswords").click(function () {
        let type = $("#inpt_newPassword1").attr("type")

        //#region show passwords
        if (type == "password") {
            $("#inpt_newPassword1").attr("type", "text");  // show
            $("#inpt_newPassword2").attr("type", "text");  // show
        }
        //#endregion

        //#region hide passwords
        else if (type == "text") {
            $("#inpt_newPassword1").attr("type", "password");  // show 
            $("#inpt_newPassword2").attr("type", "password");  // show 
        }
        //#endregion
    });


    $("form").submit(async function (e) {
        e.preventDefault();

        // save button
        await $("#btn_save").attr("disabled", "");  // disable
        await $("#btn_save").toggleClass("btn_hover");  // remove class

        //#region get passwords on inputs
        let password1 = $("#inpt_newPassword1").val();
        let password2 = $("#inpt_newPassword2").val();
        //#endregion

        //#region control passwords
        if (password1 != password2) {
            await updateResultLabel("#td_resultLabel", "#btn_save", "Şifreler Birbirine <b style= 'color:red'>Eşit Değil</b>", 3);
            return;
        }
        //#endregion

        //#region update password
        let data = {
            email: $("#btn_save").val(),
            password: $("#inpt_newPassword1").val()
        }

        // password control
        if (data.password.length < 6
            || data.password.length > 16) {
            updateResultLabel("#td_resultLabel", "#btn_save", "Yeni şifreniz <b style= 'color:red'>6</b> karakterden büyük; <b style= 'color:red'>16</b> karakterden küçük olmalıdır.", 4)
            return;
        }

        $.ajax({
            method: "PUT",
            url: "/api/user",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: async function () {
                updateResultLabel("#td_resultLabel", "#btn_save", "<b style= 'color:green'>Başarıyla</b> Değiştirildi", 3);
            },
            error: async function (response) {
                updateResultLabel("#td_resultLabel", "#btn_save", `<b style= 'color:red' ${response.responseText}</b>`, 3);
            }
        });
        //#endregion
    });
});