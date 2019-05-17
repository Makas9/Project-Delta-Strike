<?php
    $con = mysqli_connect('localhost', 'u484157030_admin', 'MercyKill2020', 'u484157030_del2');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $result = mysqli_query($con, "SELECT type, name, descripton, stats FROM item") or die(mysqli_error($con));
//echo $result;
    if ($result) {
        while ($row = $result->fetch_assoc()) {
            $objectText = "Object:" . $row["type"] . "|" . $row["name"] . "|" . $row["descripton"] . "|" . $row["image"] . "|" . $row["stats"];
            echo $objectText;
        }
    
        /* free result set */
        $result->free();
    }
    mysqli_close($con);
?>