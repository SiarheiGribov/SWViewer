<?php
$ts_pw = posix_getpwuid(posix_getuid());
$ts_mycnf = parse_ini_file($ts_pw['dir'] . "/replica.my.cnf");
$db = mysql_connect('tools.labsdb', $ts_mycnf['user'], $ts_mycnf['password']);
unset($ts_mycnf, $ts_pw);
mysql_select_db('sXXXX__count', $db); // name of my DB

$userName =  $_GET["user"];
$key =  $_GET["key"];
if((!$key) OR ($key !== "fkrMpQNmAC")) {
$query = "SELECT user FROM users";
$result = mysql_query ( $query );
echo "<html>
<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>
The users of SW-Viewer:<br>";
while($row = mysql_fetch_array($result)) {
 echo $row[user]."<br>";
}
echo "<br><a href='https://tools.wmflabs.org/iluvatarbot/'>Main page</a>";
echo "</html>";
}
else {
if ($userName) {
$query = 'SELECT count FROM users WHERE user="'.$userName.'"';
$result = mysql_query ( $query );
if (mysql_num_rows($result) > 0) {
	while($row = mysql_fetch_array($result)) {
		$now_count = $row[count];
	}
	$now_count++;
	$query = 'UPDATE users SET count="'.$now_count.'" WHERE user="'.$userName.'";';
    $result = mysql_query ( $query );
	}
    else {
        $query = 'INSERT INTO users VALUES("'.$userName.'", "1")';
        $result = mysql_query ( $query );
	}
	echo "Done!";
}
else {
	$query = 'SELECT count FROM users';
    $result = mysql_query ( $query );
	$i = 0;
       $n = mysql_num_rows($result);
	if ($n > 0) {
	while($row = mysql_fetch_array($result)) {
		$i = $i + (int)$row[count];
	}
	}
	echo $i." times by the ".$n." users.";
}
}
mysql_close($connect);
?>