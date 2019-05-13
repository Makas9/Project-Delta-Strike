<?php
    $con = mysqli_connect('tommy.heliohost.org', 'kosbud_admin', 'admin123admin', 'kosbud_uniproject');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $result = mysqli_query($con, "SELECT type, name, descripton, stats FROM item
LEFT JOIN inventory ON item.id=inventory.item
WHERE user=(SELECT id FROM user WHERE username='" . $username . "')") or die(mysqli_error($con));
//echo $result;
    if ($result) {
        while ($row = $result->fetch_assoc()) {
            $objectText = $row["name"] . "|";
            echo $objectText;
        }
    
        /* free result set */
        $result->free();
    }
    mysqli_close($con);
?>