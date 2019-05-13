<?php
    $con = mysqli_connect('localhost', 'u484157030_admin', 'MercyKill2020', 'u484157030_delst');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $password = $_POST["password"];

    // check if username exists
    $query = "SELECT * FROM `user` WHERE username='" . $username ."';";

    $namecheck = mysqli_query($con, $query) or die(mysqli_error($con));
    $rowsCount = mysqli_num_rows($namecheck);
    if ($rowsCount == 0)
    {
        echo "2: no user with such name";
    }
    else if ($rowsCount > 1)
    {
        echo "3: internal error";
        exit();
    }
    $existinginfo = mysqli_fetch_assoc($namecheck);
    $hash = $existinginfo["password"];

    if (password_verify($password, $hash)) {
        echo 'Valid';
    } else {
        echo 'Invalid password.';
    }
    
    echo "1";
    mysqli_close($con);
?>