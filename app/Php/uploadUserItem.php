<?php
    $con = mysqli_connect('tommy.heliohost.org', 'kosbud_admin', 'admin123admin', 'kosbud_uniproject');
    if (mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }
    
    $username = $_POST["username"];
    $item = $_POST["item"];
    
    $sql = "INSERT INTO `inventory` (`item`, `user`) VALUES ((SELECT `item`.id FROM `item` WHERE `item`.`name` = '" . $item ."'), (SELECT `user`.id FROM `user` WHERE `user`.`username`='". $username ."'))";
    
    if ($con->query($sql) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql . "<br>" . $con->error;
    }
    
    mysqli_close($con);
?>