export async function updateResultLabel(label, submitButton, message, timeout) {
    await $(label).empty();
    await $(label).append(`<i>${message}</i>`);

    //#region reset result label
    await setTimeout(
        () => {
            $(label).empty()

            // save button
            $(submitButton).removeAttr("disabled");  // enable
            $(submitButton).toggleClass("btn_hover");  // add class
        },
        timeout * 1000
    )
    //#endregion
} 