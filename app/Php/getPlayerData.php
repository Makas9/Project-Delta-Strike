<?php
    $con = mysqli_connect('tommy.heliohost.org', 'kosbud_admin', 'admin123admin', 'kosbud_uniproject');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $data = $_POST["data"];
    $sql = "SELECT FROM `playerData` username, data WHERE username=" . $username;
    $result = mysqli_query($con, $sql);
    //echo $result;
    if ($result) {
        $row = $result->fetch_assoc();
        $objectText = $row["data"];
        echo $objectText;
    
        /* free result set */
        $result->free();
    }
    mysqli_close($con);
?>