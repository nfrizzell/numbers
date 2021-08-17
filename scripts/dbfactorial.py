import mysql.connector
from getpass import getpass

with mysql.connector.connect(host="localhost", user=input("User: "), password=getpass("Pass: "), database="numbers") as connection:
	cursor = connection.cursor()
	query = ("INSERT INTO big_factorial "
		"(input, value)"
		"VALUES (%s, %s)")
	val = 1
	for i in range(2, 10000):
		val = val * i
		data = (str(i), str(val))
		cursor.execute(query, data)

	connection.commit()
