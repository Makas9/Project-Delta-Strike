<?php
    $con = mysqli_connect('localhost', 'u484157030_admin', 'MercyKill2020', 'u484157030_delst');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $data = mysqli_real_escape_string($con, $_POST["data"]);
    $sql = "INSERT INTO `playerData` (username, data) VALUES ('" . $username ."','" . $data . "') ON DUPLICATE KEY UPDATE username='". $username ."', data='" . $data . "'";
    if ($con->query($sql) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql . "<br>" . $con->error;
    }
    mysqli_close($con);
?>