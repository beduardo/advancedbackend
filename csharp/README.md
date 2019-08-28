#ADVANCED TEST 

To run this test with docker:
docker run -itd -p 5000:5000 beduardo/advancedtest:tagname

to query by city name:
http://localhost:5000/citymusic?d=cityname

to query by coordinates:
http://localhost:5000/citymusic?lat=99&lon=99