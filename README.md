# asp-net-booking
ASP.NET Cinema Booking System set up using MVC Template from Visual Studio with built-in authentication.

> **Note:** This project was created on Windows 10 with Visual Studio.

## How to run the project with Visual Studio
1. Clone the repository
2. Open the project in Visual Studio (or other preferable code editor), and apply database updates. In Visual Studio, open `Tools -> NuGet Package Manager -> Open Console` and type:
```
Update-Database -Context BookingCinemaContext
```
3. Apply some movies data to the database. In Visual Studio click on `View -> SQL Server Object Explorer`, find BookingCinemaContext, right click on it and hit New Query option. Then apply the following SQL Query:
```
INSERT INTO Movie VALUES 
	('Matrix', '1999-01-01 12:00:00', 'Sci-Fi', 10, 0, 'https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcS4jfQQt_0vCA4XSwGiWkffC32Tv2oajdWhaYHHVYylYGJ3v17Q'),
	('Forrest Gump', '1994-01-01 12:00:00', 'Comedy', 12, null, 'https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg'),
	('The Fly', '1986-01-01 12:00:00', 'Horror', 8, null, 'https://m.media-amazon.com/images/M/MV5BODcxMGMwOGEtMDUxMi00MzE5LTg4YTYtYjk1YjA4MzQxNTNlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg'),
	('Raiders of the lost ark', '1981-01-01 12:00:00', 'Action', 10, null, 'https://m.media-amazon.com/images/I/61H2YD-bubL._AC_SY1000_.jpg');
```
> **Note:** This is just an example query adding a couple of movies to the Movie table. This is an optional step, that would make using the application easier with pre-loaded movies.

4. Run project (in Visual Studio preferably using IIS Express). This should connect automatically with the database and run application by default on the localhost.

## Project preview

![Movies List](preview-assets/screenshot1.png)

![Booking a seat](preview-assets/screenshot2.png)

![Booked Movies](preview-assets/screenshot3.png)