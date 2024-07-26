window.ShowToastr = (type, message) => {
    //if (type === "success") {
    //    Swal.fire({
    //        position: 'top-end',
    //        icon: 'success',
    //        title: message,
    //        showConfirmButton: false,
    //        timer: 1500
    //    })
    //}
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        )
    }
    //if (type === "error") {
    //    Swal.fire({
    //        position: 'top-end',
    //        icon: 'error',
    //        title: message,
    //        showConfirmButton: false,
    //        timer: 5000
    //    })
    //}
}

