using System;

public static class DatabaseSeeder
{
    private static readonly string ConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["HotelDbContext"].ConnectionString;

    public static void SeedDatabase()
    {
        using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
        {
            connection.Open();

            // Check if database already has data
            bool needsSeeding = CheckIfSeedingIsNeeded(connection);

            if (needsSeeding)
            {
                // Start a transaction for all operations
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert hotels
                        ExecuteSql(connection, transaction, @"
                            INSERT INTO Hotels (Name, Address, City, Country, Stars, ContactPhone, Email, IsActive) 
                            VALUES 
                            ('Grand Plaza Hotel', '123 Main Street', 'Bucharest', 'Romania', 5, '+40-721-123-456', 'info@grandplaza.ro', 1),
                            ('Seaside Resort', '45 Beach Boulevard', 'Constanta', 'Romania', 4, '+40-722-345-678', 'contact@seasideresort.ro', 1),
                            ('Mountain View Lodge', '78 Pine Street', 'Brasov', 'Romania', 3, '+40-723-567-890', 'info@mountainview.ro', 1),
                            ('City Center Hotel', '15 Victory Square', 'Cluj-Napoca', 'Romania', 4, '+40-724-678-901', 'info@citycenter.ro', 1),
                            ('Business Hotel', '32 Republic Street', 'Timisoara', 'Romania', 4, '+40-725-789-012', 'contact@businesshotel.ro', 1);
                        ");

                        // Insert room types
                        ExecuteSql(connection, transaction, @"
                            INSERT INTO RoomTypes (Name, Description, MaxOccupancy, BasePrice) 
                            VALUES 
                            ('Single Room', 'A room assigned to one person.', 1, 80.00),
                            ('Double Room', 'A room assigned to two people.', 2, 120.00),
                            ('Twin Room', 'A room with two single beds.', 2, 130.00),
                            ('Triple Room', 'A room that can accommodate three persons.', 3, 160.00),
                            ('Quad Room', 'A room assigned to four people.', 4, 190.00),
                            ('Suite', 'A luxurious room with separate living area.', 4, 250.00),
                            ('Junior Suite', 'A single room with a bed and sitting area.', 2, 200.00),
                            ('Presidential Suite', 'The most expensive room of the hotel.', 6, 500.00);
                        ");

                        // Insert rooms
                        ExecuteSql(connection, transaction, @"
                            -- Get hotel IDs
                            DECLARE @Hotel1Id INT = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Grand Plaza Hotel');
                            DECLARE @Hotel2Id INT = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Seaside Resort');
                            DECLARE @Hotel3Id INT = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Mountain View Lodge');
                            DECLARE @Hotel4Id INT = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'City Center Hotel');
                            DECLARE @Hotel5Id INT = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Business Hotel');
                            
                            -- Get room type IDs
                            DECLARE @SingleRoomId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Single Room');
                            DECLARE @DoubleRoomId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Double Room');
                            DECLARE @TwinRoomId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Twin Room');
                            DECLARE @TripleRoomId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Triple Room');
                            DECLARE @QuadRoomId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Quad Room');
                            DECLARE @SuiteId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Suite');
                            DECLARE @JuniorSuiteId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Junior Suite');
                            DECLARE @PresidentialSuiteId INT = (SELECT TOP 1 Id FROM RoomTypes WHERE Name = 'Presidential Suite');
                            
                            -- Insert rooms for Grand Plaza Hotel
                            INSERT INTO Rooms (RoomNumber, HotelId, RoomTypeId, Floor, IsClean, IsOccupied, NeedsRepair, Notes)
                            VALUES 
                            ('101', @Hotel1Id, @SingleRoomId, 1, 1, 0, 0, 'Quiet room with garden view'),
                            ('102', @Hotel1Id, @SingleRoomId, 1, 1, 0, 0, 'Standard room'),
                            ('201', @Hotel1Id, @DoubleRoomId, 2, 1, 0, 0, 'Corner room with extra space'),
                            ('202', @Hotel1Id, @TwinRoomId, 2, 1, 0, 0, 'Twin beds with city view'),
                            ('301', @Hotel1Id, @JuniorSuiteId, 3, 1, 0, 0, 'Junior suite with balcony'),
                            ('302', @Hotel1Id, @SuiteId, 3, 1, 0, 0, 'Suite with living room'),
                            ('401', @Hotel1Id, @PresidentialSuiteId, 4, 1, 0, 0, 'Presidential suite with balcony and city views');
                            
                            -- Insert rooms for Seaside Resort
                            INSERT INTO Rooms (RoomNumber, HotelId, RoomTypeId, Floor, IsClean, IsOccupied, NeedsRepair, Notes)
                            VALUES 
                            ('101', @Hotel2Id, @SingleRoomId, 1, 1, 0, 0, 'Garden view'),
                            ('102', @Hotel2Id, @DoubleRoomId, 1, 1, 0, 0, 'Standard double'),
                            ('103', @Hotel2Id, @TwinRoomId, 1, 1, 0, 0, 'Twin room with terrace'),
                            ('201', @Hotel2Id, @TripleRoomId, 2, 1, 0, 0, 'Family room with balcony'),
                            ('202', @Hotel2Id, @QuadRoomId, 2, 1, 0, 0, 'Large room for families'),
                            ('301', @Hotel2Id, @SuiteId, 3, 1, 0, 0, 'Sea view suite');
                            
                            -- Insert rooms for Mountain View Lodge
                            INSERT INTO Rooms (RoomNumber, HotelId, RoomTypeId, Floor, IsClean, IsOccupied, NeedsRepair, Notes)
                            VALUES 
                            ('101', @Hotel3Id, @SingleRoomId, 1, 1, 0, 0, 'Mountain view'),
                            ('102', @Hotel3Id, @DoubleRoomId, 1, 1, 0, 0, 'Cozy room'),
                            ('201', @Hotel3Id, @TwinRoomId, 2, 1, 0, 0, 'Twin room with fireplace'),
                            ('202', @Hotel3Id, @SuiteId, 2, 1, 0, 0, 'Suite with kitchenette');
                            
                            -- Insert rooms for City Center Hotel
                            INSERT INTO Rooms (RoomNumber, HotelId, RoomTypeId, Floor, IsClean, IsOccupied, NeedsRepair, Notes)
                            VALUES 
                            ('101', @Hotel4Id, @SingleRoomId, 1, 1, 0, 0, 'Standard single'),
                            ('102', @Hotel4Id, @DoubleRoomId, 1, 1, 0, 0, 'Business double'),
                            ('103', @Hotel4Id, @TwinRoomId, 1, 1, 0, 0, 'Twin room for business travelers');
                            
                            -- Insert rooms for Business Hotel
                            INSERT INTO Rooms (RoomNumber, HotelId, RoomTypeId, Floor, IsClean, IsOccupied, NeedsRepair, Notes)
                            VALUES 
                            ('101', @Hotel5Id, @SingleRoomId, 1, 1, 0, 0, 'Business single with workspace'),
                            ('102', @Hotel5Id, @DoubleRoomId, 1, 1, 0, 0, 'Double room with office desk'),
                            ('201', @Hotel5Id, @JuniorSuiteId, 2, 1, 0, 0, 'Junior suite with meeting area');
                        ");

                        // Insert customers
                        ExecuteSql(connection, transaction, @"
                            INSERT INTO Customers (FirstName, LastName, Email, Phone, Address, City, Country, IdNumber, DateOfBirth, RegistrationDate, IsVIP)
                            VALUES
                            ('John', 'Doe', 'john.doe@example.com', '+40-721-111-222', '123 Customer Street', 'Bucharest', 'Romania', 'XX123456', '1985-05-15', DATEADD(month, -3, GETDATE()), 0),
                            ('Jane', 'Smith', 'jane.smith@example.com', '+40-722-333-444', '456 Customer Avenue', 'Cluj-Napoca', 'Romania', 'XX789012', '1990-08-22', DATEADD(month, -2, GETDATE()), 1),
                            ('Maria', 'Popescu', 'maria.popescu@example.com', '+40-723-555-666', '789 Main Street', 'Brasov', 'Romania', 'XX345678', '1978-12-10', DATEADD(month, -6, GETDATE()), 0),
                            ('Robert', 'Johnson', 'robert.johnson@example.com', '+40-724-777-888', '321 Park Road', 'Constanta', 'Romania', 'XX901234', '1982-03-25', DATEADD(month, -1, GETDATE()), 0),
                            ('Elena', 'Ionescu', 'elena.ionescu@example.com', '+40-725-999-000', '654 Lake View', 'Timisoara', 'Romania', 'XX567890', '1995-07-18', DATEADD(month, -4, GETDATE()), 1),
                            ('Michael', 'Brown', 'michael.brown@example.com', '+40-726-111-222', '987 River Road', 'Iasi', 'Romania', 'XX234567', '1988-09-30', DATEADD(month, -5, GETDATE()), 0);
                        ");

                        // Insert services
                        ExecuteSql(connection, transaction, @"
                            INSERT INTO Services (Name, Description, Price, Category, IsAvailable)
                            VALUES
                            ('Room Service', 'Food and beverage service delivered to the room', 20.00, 'RoomService', 1),
                            ('Breakfast Buffet', 'Full breakfast buffet in the restaurant', 15.00, 'RoomService', 1),
                            ('Lunch Menu', 'Three-course lunch in the restaurant', 25.00, 'RoomService', 1),
                            ('Dinner Menu', 'Five-course dinner in the restaurant', 35.00, 'RoomService', 1),
                            ('Spa Access', 'Access to the hotel''s spa facilities', 50.00, 'Spa', 1),
                            ('Massage', '60-minute full body massage', 70.00, 'Spa', 1),
                            ('Facial Treatment', '45-minute facial treatment', 60.00, 'Spa', 1),
                            ('Airport Transfer', 'Transportation between hotel and airport', 30.00, 'Transportation', 1),
                            ('Taxi Service', 'Taxi booking through the hotel', 10.00, 'Transportation', 1),
                            ('Room Cleaning', 'Extra room cleaning service', 15.00, 'Cleaning', 1),
                            ('Laundry Service', 'Same-day laundry service', 25.00, 'Cleaning', 1),
                            ('Business Center', 'Access to business center facilities', 20.00, 'Business', 1),
                            ('Conference Room', '4-hour conference room rental', 100.00, 'Business', 1),
                            ('Gym Access', 'Access to the hotel''s fitness center', 10.00, 'Recreation', 1),
                            ('Swimming Pool', 'Access to swimming pool area', 15.00, 'Recreation', 1),
                            ('Babysitting', 'Childcare services', 25.00, 'Miscellaneous', 1),
                            ('Late Check-out', 'Extended check-out until 4 PM', 30.00, 'Miscellaneous', 1);
                        ");

                        // Insert some sample reservations
                        ExecuteSql(connection, transaction, @"
                            -- Declare customer IDs
                            DECLARE @Customer1Id INT = (SELECT TOP 1 Id FROM Customers WHERE Email = 'john.doe@example.com');
                            DECLARE @Customer2Id INT = (SELECT TOP 1 Id FROM Customers WHERE Email = 'jane.smith@example.com');
                            
                            -- Declare room IDs
                            DECLARE @Room1Id INT = (SELECT TOP 1 Id FROM Rooms WHERE RoomNumber = '101' AND HotelId = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Grand Plaza Hotel'));
                            DECLARE @Room2Id INT = (SELECT TOP 1 Id FROM Rooms WHERE RoomNumber = '102' AND HotelId = (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Seaside Resort'));
                            
                            -- Create past reservations
                            INSERT INTO Reservations (CustomerId, RoomId, HotelId, CheckInDate, CheckOutDate, NumberOfGuests, TotalPrice, ReservationStatus, CreatedAt, Notes)
                            VALUES
                            (@Customer1Id, @Room1Id, (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Grand Plaza Hotel'), 
                             DATEADD(month, -1, GETDATE()), DATEADD(month, -1, DATEADD(day, 3, GETDATE())), 
                             1, 240.00, 'Completed', DATEADD(month, -2, GETDATE()), 'Past reservation'),
                             
                            (@Customer2Id, @Room2Id, (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Seaside Resort'), 
                             DATEADD(month, -2, GETDATE()), DATEADD(month, -2, DATEADD(day, 5, GETDATE())), 
                             2, 600.00, 'Completed', DATEADD(month, -3, GETDATE()), 'VIP customer');
                             
                            -- Create upcoming reservations for next month
                            INSERT INTO Reservations (CustomerId, RoomId, HotelId, CheckInDate, CheckOutDate, NumberOfGuests, TotalPrice, ReservationStatus, CreatedAt, Notes)
                            VALUES
                            (@Customer1Id, @Room2Id, (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Seaside Resort'), 
                             DATEADD(month, 1, GETDATE()), DATEADD(month, 1, DATEADD(day, 7, GETDATE())), 
                             2, 840.00, 'Confirmed', GETDATE(), 'Upcoming summer vacation'),
                             
                            (@Customer2Id, @Room1Id, (SELECT TOP 1 Id FROM Hotels WHERE Name = 'Grand Plaza Hotel'), 
                             DATEADD(month, 2, GETDATE()), DATEADD(month, 2, DATEADD(day, 3, GETDATE())), 
                             1, 240.00, 'Confirmed', GETDATE(), 'Business trip');
                        ");

                        // Create sample invoices
                        ExecuteSql(connection, transaction, @"
                            -- Declare reservation IDs for completed reservations
                            DECLARE @Reservation1Id INT = (SELECT TOP 1 Id FROM Reservations WHERE ReservationStatus = 'Completed' ORDER BY Id);
                            DECLARE @Reservation2Id INT = (SELECT TOP 1 Id FROM Reservations WHERE ReservationStatus = 'Completed' AND Id != @Reservation1Id ORDER BY Id);
                            
                            -- Generate invoices for completed reservations
                            INSERT INTO Invoices (ReservationId, InvoiceNumber, CustomerName, CustomerAddress, CustomerCity, CustomerCountry, 
                                                 HotelName, HotelAddress, RoomInfo, CheckInDate, CheckOutDate, IssueDate, DueDate, 
                                                 SubTotal, Tax, Total, PaymentMethod, Notes, IsPaid)
                            VALUES
                            (@Reservation1Id, 'INV-2023-001', 
                             (SELECT FirstName + ' ' + LastName FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT Address FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT City FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT Country FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT Name FROM Hotels WHERE Id = (SELECT HotelId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT Address FROM Hotels WHERE Id = (SELECT HotelId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT RoomNumber + ' - ' + RT.Name FROM Rooms R 
                              JOIN RoomTypes RT ON R.RoomTypeId = RT.Id 
                              WHERE R.Id = (SELECT RoomId FROM Reservations WHERE Id = @Reservation1Id)),
                             (SELECT CheckInDate FROM Reservations WHERE Id = @Reservation1Id),
                             (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation1Id),
                             DATEADD(day, -1, (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation1Id)),
                             DATEADD(day, 14, (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation1Id)),
                             200.00, 40.00, 240.00, 'Credit Card', 'Paid at check-out', 1),
                             
                            (@Reservation2Id, 'INV-2023-002', 
                             (SELECT FirstName + ' ' + LastName FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT Address FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT City FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT Country FROM Customers WHERE Id = (SELECT CustomerId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT Name FROM Hotels WHERE Id = (SELECT HotelId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT Address FROM Hotels WHERE Id = (SELECT HotelId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT RoomNumber + ' - ' + RT.Name FROM Rooms R 
                              JOIN RoomTypes RT ON R.RoomTypeId = RT.Id 
                              WHERE R.Id = (SELECT RoomId FROM Reservations WHERE Id = @Reservation2Id)),
                             (SELECT CheckInDate FROM Reservations WHERE Id = @Reservation2Id),
                             (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation2Id),
                             DATEADD(day, -1, (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation2Id)),
                             DATEADD(day, 14, (SELECT CheckOutDate FROM Reservations WHERE Id = @Reservation2Id)),
                             500.00, 100.00, 600.00, 'Cash', 'VIP customer', 1);
                        ");

                        // Commit all changes
                        transaction.Commit();
                        System.Diagnostics.Debug.WriteLine("Database seeded successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Roll back all changes if there's an error
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"Error seeding database: {ex.Message}");
                        throw;
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database already contains data. Skipping seed operation.");
            }
        }
    }

    private static bool CheckIfSeedingIsNeeded(System.Data.SqlClient.SqlConnection connection)
    {
        try
        {
            using (var command = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Hotels", connection))
            {
                int count = (int)command.ExecuteScalar();
                return count == 0;
            }
        }
        catch
        {
            // Table might not exist yet
            return true;
        }
    }

    private static void ExecuteSql(System.Data.SqlClient.SqlConnection connection,
                                  System.Data.SqlClient.SqlTransaction transaction,
                                  string sql)
    {
        using (var command = new System.Data.SqlClient.SqlCommand(sql, connection, transaction))
        {
            command.ExecuteNonQuery();
        }
    }
}