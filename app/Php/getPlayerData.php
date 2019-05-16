<?php
    $con = mysqli_connect('localhost', 'u484157030_admin', 'MercyKill2020', 'u484157030_del2');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $data = $_POST["data"];
    $sql = "SELECT username, data FROM `playerData` WHERE username='" . $username . "'";
    $result = mysqli_query($con, $sql);
    //echo $result;
    if ($result) {
        $row = $result->fetch_assoc();
        $objectText = $row["data"];
        echo $objectText;
    
        /* free result set */
        $result->free();
    }
    else {
        echo mysqli_connect_error();
    }
    mysqli_close($con);
?>