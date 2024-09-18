using AppData.Enum;
using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace shop.Infrastructure.Initialization
{
    public class Seeding
    {
        public static void SeedingAccount(ModelBuilder modelBuilder)
        {
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash1, out byte[] passwordSalt1);
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash2, out byte[] passwordSalt2);
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash3, out byte[] passwordSalt3);
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash4, out byte[] passwordSalt4);

            modelBuilder.Entity<RoleEntity>().HasData(
                     new RoleEntity
                     {
                         Id = new Guid("80a02536-2e92-466f-914f-8f1c61d01fd5"),
                         RoleName = "Admin"
                     },
                     new RoleEntity
                     {
                         Id = new Guid("9ebee0d5-323a-4052-af12-827a9e856639"),
                         RoleName = "Customer"
                     },
                     new RoleEntity
                     {
                         Id = new Guid("5b3a05b0-c011-4593-abd1-cb2e486f8e43"),
                         RoleName = "Employee"
                     }
             );

            modelBuilder.Entity<AccountEntity>().HasData(
                   new AccountEntity
                   {
                       Id = new Guid("3aec8f0b-3a6a-4b5d-8a3a-348ae529001a"),
                       Username = "admin@example.com",
                       PasswordHash = passwordHash1,
                       PasswordSalt = passwordSalt1,
                       RoleId = new Guid("80a02536-2e92-466f-914f-8f1c61d01fd5")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("2b25a754-a50e-4468-942c-d65c0bc2c86f"),
                       Username = "customer@example.com",
                       PasswordHash = passwordHash2,
                       PasswordSalt = passwordSalt2,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639"),
                   },
                   new AccountEntity
                   {
                       Id = new Guid("db757696-89d6-4f61-84bb-61bc9b87ea05"),
                       Username = "customer2@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639"),
                   },
                   new AccountEntity
                   {
                       Id = new Guid("3141069d-f4f3-475c-8efc-99e1b4c3e627"),
                       Username = "employee@example.com",
                       PasswordHash = passwordHash3,
                       PasswordSalt = passwordSalt3,
                       RoleId = new Guid("5b3a05b0-c011-4593-abd1-cb2e486f8e43")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("a3339dd7-94b9-4f12-9d18-2ee341b4f35c"),
                       Username = "nguyenvanminh@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("6060ab5a-ca8b-409c-87b2-363a69f06e66"),
                       Username = "lethimai@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("0e5c838e-f387-4183-a1c1-4c1e802ab180"),
                       Username = "tranlan@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("ae4c4f03-aa8a-4f37-a7cb-c5bc06e08d74"),
                       Username = "nguyenan@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("463d52ee-4c4e-40b0-a8f3-e59086878964"),
                       Username = "phantuyet@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("80cd99a5-f3e4-43f6-a725-f4e07fa7cd7d"),
                       Username = "vuvankhai@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   },
                   new AccountEntity
                   {
                       Id = new Guid("c36aab76-f6cc-46f6-a6c3-730d54b61a48"),
                       Username = "lehoanganh@example.com",
                       PasswordHash = passwordHash4,
                       PasswordSalt = passwordSalt4,
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639")
                   }, 
                   new AccountEntity
                   {
                       Id = new Guid("4b45812f-2f47-41b9-b913-39bed1b02c1d"),
                       Username = "tranle@example.com", 
                       PasswordHash = passwordHash4, 
                       PasswordSalt = passwordSalt4, 
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639") 
                   }, 
                   new AccountEntity
                   {
                       Id = new Guid("3a9477da-b75c-4ef6-9bf6-a93aa5ffaf6f"),
                       Username = "hoangmai@example.com", 
                       PasswordHash = passwordHash4, 
                       PasswordSalt = passwordSalt4, 
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639") 
                   }, 
                   new AccountEntity
                   {
                       Id = new Guid("d15fcb08-fcb1-4a55-b012-b2be211ed2c1"),
                       Username = "lehoa@example.com", 
                       PasswordHash = passwordHash4, 
                       PasswordSalt = passwordSalt4, 
                       RoleId = new Guid("9ebee0d5-323a-4052-af12-827a9e856639") 
                   }
                 );

            modelBuilder.Entity<AddressEntity>().HasData(
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("3aec8f0b-3a6a-4b5d-8a3a-348ae529001a"),
                       Name = "John Admin",
                       Email = "johnadmin@example.com",
                       PhoneNumber = "1234567891",
                       Address = "125 Đường Cầu Giấy ,Cầu Giấy, Hà Nội",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("2b25a754-a50e-4468-942c-d65c0bc2c86f"),
                       Name = "Đăng Thị Hồng Nhung",
                       Email = "dangnhung72@gmail.com",
                       PhoneNumber = "0366702305",
                       Address = "25 Phình Hồ, Văn Chấn, Yên Bái",
                       IsMain = false
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("2b25a754-a50e-4468-942c-d65c0bc2c86f"),
                       Name = "Nguyễn Quang Hưng",
                       Email = "q170302@email.com",
                       PhoneNumber = "0344917302",
                       Address = "22 Ngã Ba Kim, Mù Cang Chải, Yên Bái",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("db757696-89d6-4f61-84bb-61bc9b87ea05"),
                       Name = "Nguyễn Văn An",
                       Email = "nguyenvana@example.com",
                       PhoneNumber = "0912345678",
                       Address = "15 Lê Văn Lương, Thanh Xuân, Hà Nội",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("db757696-89d6-4f61-84bb-61bc9b87ea05"),
                       Name = "Lê Thị Bích",
                       Email = "lethib@example.com",
                       PhoneNumber = "0923456789",
                       Address = "20 Nguyễn Trãi, Thanh Xuân, Hà Nội",
                       IsMain = false
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("db757696-89d6-4f61-84bb-61bc9b87ea05"),
                       Name = "Phạm Văn Công",
                       Email = "phamvanc@example.com",
                       PhoneNumber = "0934567890",
                       Address = "88 Đường Giải Phóng, Hoàng Mai, Hà Nội",
                       IsMain = false
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("3141069d-f4f3-475c-8efc-99e1b4c3e627"),
                       Name = "John Employee",
                       Email = "johnemployee@example.com",
                       PhoneNumber = "1234567892",
                       Address = "121 Đường Cầu Giấy ,Cầu Giấy, Hà Nội",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("a3339dd7-94b9-4f12-9d18-2ee341b4f35c"),
                       Name = "Nguyễn Văn Minh",
                       Email = "minhnguyen@example.com",
                       PhoneNumber = "0987654321",
                       Address = "456 Đường Nguyễn Trãi, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("6060ab5a-ca8b-409c-87b2-363a69f06e66"),
                       Name = "Lê Thị Mai",
                       Email = "lethimai@example.com",
                       PhoneNumber = "0912345679",
                       Address = "789 Đường Lê Văn Sỹ, Phường 13, Quận 3, TP. Hồ Chí Minh",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("0e5c838e-f387-4183-a1c1-4c1e802ab180"),
                       Name = "Trần Thị Lan",
                       Email = "tranlan@example.com",
                       PhoneNumber = "0908765432",
                       Address = "123 Đường Lê Lai, Phường Phú Hòa, TP. Thủ Dầu Một, Bình Dương",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("ae4c4f03-aa8a-4f37-a7cb-c5bc06e08d74"),
                       Name = "Nguyễn Thị An",
                       Email = "nguyenan@example.com",
                       PhoneNumber = "0976543210",
                       Address = "456 Đường Nguyễn Thái Học, Phường 10, TP. Cần Thơ",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("463d52ee-4c4e-40b0-a8f3-e59086878964"),
                       Name = "Phan Thị Tuyết",
                       Email = "phantuyet@example.com",
                       PhoneNumber = "0901234567",
                       Address = "789 Đường Võ Văn Tần, Phường 5, Quận 3, TP. Hồ Chí Minh",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("80cd99a5-f3e4-43f6-a725-f4e07fa7cd7d"),
                       Name = "Vũ Văn Khải",
                       Email = "vuvankhai@example.com",
                       PhoneNumber = "0912345678",
                       Address = "123 Đường Trường Chinh, Phường 14, Quận Tân Bình, TP. Hồ Chí Minh",
                       IsMain = true
                   },
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("c36aab76-f6cc-46f6-a6c3-730d54b61a48"),
                       Name = "Lê Hoàng Anh",
                       Email = "lehoanganh@example.com",
                       PhoneNumber = "0923456789",
                       Address = "234 Đường Hà Huy Tập, Phường Đông Vệ, TP. Thanh Hóa",
                       IsMain = true
                   }, 
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       AccountId = new Guid("4b45812f-2f47-41b9-b913-39bed1b02c1d"),
                       Name = "Trần Thị Lệ",
                       Email = "tranle@example.com",
                       PhoneNumber = "0987654321",
                       Address = "123 Đường Nguyễn Văn Linh, Phường Tân Hưng, Quận 7, TP. Hồ Chí Minh",
                       IsMain = true
                   }, 
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(), 
                       AccountId = new Guid("3a9477da-b75c-4ef6-9bf6-a93aa5ffaf6f"),
                       Name = "Hoàng Thị Mai",
                       Email = "hoangmai@example.com",
                       PhoneNumber = "0976543210",
                       Address = "456 Đường Phan Chu Trinh, Phường 5, TP. Đà Nẵng",
                       IsMain = true
                   }, 
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(), 
                       AccountId = new Guid("d15fcb08-fcb1-4a55-b012-b2be211ed2c1"),
                       Name = "Lê Thị Hòa",
                       Email = "lehoa@example.com",
                       PhoneNumber = "0934567890",
                       Address = "789 Đường Lý Thường Kiệt, Phường Bắc Lý, TP. Quảng Bình",
                       IsMain = true
                   }

                 );
        }

        public static void SeedingBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>().HasData(
               new PaymentMethod
               {
                   Id = Guid.NewGuid(),
                   Name = "Thanh toán khi nhận hàng (COD)"
               },
               new PaymentMethod
               {
                   Id = Guid.NewGuid(),
                   Name = "Thanh toán tiền mặt tại quầy"
               },
               new PaymentMethod
               {
                   Id = Guid.NewGuid(),
                   Name = "Chuyển khoản"
               },
               new PaymentMethod
               {
                   Id = Guid.NewGuid(),
                   Name = "Ví điện tử (VNPay)"
               }
               );
        }

        public static void SeedingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
               new ProductType
               {
                   Id = new Guid("e21526c4-f78b-4eb5-8ae0-919633179582"),
                   Name = "Model LD2410",
               },
               new ProductType
               {
                   Id = new Guid("ff58b31e-53d5-4216-84a4-0566cb2e9b52"),
                   Name = "Model ATMEGA16U2",
               },
               new ProductType
               {
                   Id = new Guid("b28481c3-51fb-4a94-8558-0ac0ebfb1607"),
                   Name = "Model CH340G",
               },
               new ProductType
               {
                   Id = new Guid("43a56b31-2f02-4e9d-88b0-2b3ced2276ba"),
                   Name = "Model A",
               },
               new ProductType
               {
                   Id = new Guid("dfd52a60-8ccf-4ac2-980c-14ddf41e9a18"),
                   Name = "Bản thường",
               },
               new ProductType
               {
                   Id = new Guid("fbaeaba4-a5bc-487e-b0e5-0967ff543d5d"),
                   Name = "Kèm pin",
               },
               new ProductType
               {
                   Id = new Guid("7c604dd7-d603-4f6a-b5bf-f254bd812787"),
                   Name = "Model ACD269",
               },
               new ProductType
               {
                   Id = new Guid("e2d9219f-c57b-4c59-bea0-6a3af2e655a5"),
                   Name = "Model LD1801",
               },
               new ProductType
               {
                   Id = new Guid("f728c3bf-b4d8-41f6-93ab-7a91cba390be"),
                   Name = "Model LD2410",
               },
               new ProductType
               {
                   Id = new Guid("934902b5-c2a2-4fbc-97c2-8887ed45d08e"),
                   Name = "Model LD2410b",
               }
               );

            modelBuilder.Entity<ProductAttribute>().HasData(
                new ProductAttribute
                {
                    Id = new Guid("bd2d16dc-10a9-40b1-b76a-e39f3c015086"),
                    Name = "Vi điều khiển",
                },
                new ProductAttribute
                {
                    Id = new Guid("09ec0726-d537-4c92-aaaf-760f19c6999f"),
                    Name = "Điện áp hoạt động",
                },
                new ProductAttribute
                {
                    Id = new Guid("d369dc76-92cf-417a-aea5-17616c87d4ce"),
                    Name = "Điện áp đầu vào (khuyên dùng)"
                },
                new ProductAttribute
                {
                    Id = new Guid("50b7176f-4b13-484b-aec4-edf9383b9232"),
                    Name = "Điện áp đầu vào (giới hạn)"
                },
                new ProductAttribute
                {
                    Id = new Guid("b96ab5d0-3155-4688-8fb4-c6427e0661d5"),
                    Name = "Công suất"
                },
                new ProductAttribute
                {
                    Id = new Guid("99bf3d94-a248-46f8-bbcb-0f9ae07ce1af"),
                    Name = "Dải nhiệt độ"
                },
                new ProductAttribute
                {
                    Id = new Guid("a5913406-23e7-4451-a19c-242c974e312e"),
                    Name = "Màn hình hiển thị"
                },
                new ProductAttribute
                {
                    Id = new Guid("c84ec2fc-651b-42e1-b073-022596ac90c0"),
                    Name = "Công nghệ cảm biến"
                },
                new ProductAttribute
                {
                    Id = new Guid("4150124b-2a58-4d98-8abe-f380e99a6fa9"),
                    Name = "Góc phát hiện"
                }
                );

            modelBuilder.Entity<Category>().HasData(
              new Category
              {
                  Id = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f"),
                  Title = "Arduino",
                  Slug = "arduino"
              },
              new Category
              {
                  Id = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                  Title = "Tay hàn",
                  Slug = "tay-han"
              },
              new Category
              {
                  Id = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                  Title = "Cảm biến",
                  Slug = "cam-bien"
              }
            );

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                Title = "KIT Arduino Mega2560",
                Slug = "kit-arduino-mega2560",
                Description = "Tính năng: Nhiều chân I/O(54 chân Digital I/O và 16 chân Analog Input), Bộ nhớ lớn(256 KB bộ nhớ Flash, 8 KB SRAM và 4 KB EEPROM), Hỗ trợ PWM & cổng USB",
                ImageUrl = "https://i.pinimg.com/564x/28/3d/b1/283db15665e274cb4e6d83741238e2ae.jpg",
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f"),
                SeoTitle = "KIT Arduino Mega2560",
                SeoKeyworks = "KIT Arduino Mega2560",
                SeoDescription = "Tính năng: Nhiều chân I/O(54 chân Digital I/O và 16 chân Analog Input)"
            },
            new Product
            {
                Id = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                Title = "Kit Arduino UNO R3",
                Slug = "kit-arduino-uno-r3",
                Description = "Tính năng: Tương thích với nhiều loại shield (bảng mở rộng) và phụ kiện, Đa dạng chân I/O( với 14 chân Digital I/O (bao gồm 6 chân PWM) và 6 chân Analog Input), 32KB bộ nhớ Flash, 2 KB SRAM và 1 KB EEPROM",
                ImageUrl = "https://i.pinimg.com/564x/19/3a/65/193a65211060c29ffbddac99ea01ac59.jpg",
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f"),
                SeoTitle = "Kit Arduino UNO R3",
                SeoKeyworks = "Kit Arduino UNO R3",
                SeoDescription = "Tính năng: Tương thích với nhiều loại shield (bảng mở rộng) và phụ kiện"
            },
            new Product
            {
                Id = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                Title = "Kit Arduino WiFi ESP-32",
                Slug = "kit-arduino-wifi-esp-32",
                Description = "Tính năng: Hỗ trợ Wi-Fi 802.11 b/g/n và Bluetooth v4.2, Hiệu suất cao( với bộ xử lý dual-core 32-bit Xtensa LX6, hoạt động ở tần số lên đến 240 MH), Bộ nhớ lớn( với 520 KB SRAM và tùy chọn bộ nhớ Flash từ 4MB trở lên)",
                ImageUrl = "https://i.pinimg.com/564x/47/68/73/476873b27d117bb46f76f1bb50b73499.jpg",
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f"),
                SeoTitle = "Kit Arduino WiFi ESP-32",
                SeoKeyworks = "Kit Arduino WiFi ESP-32",
                SeoDescription = "Tính năng: Hỗ trợ Wi-Fi 802.11 b/g/n và Bluetooth v4.2, Hiệu suất cao( với bộ xử lý dual-core 32-bit Xtensa LX6, hoạt động ở tần số lên đến 240 MH)"
            },
            new Product
            {
                Id = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "Bộ Trạm Hàn Makita C11",
                Slug = "bo-tram-han-makita-c11",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/22/db/f1/22dbf1e839f495516eba3842d35c7948.jpg",
                SeoTitle = "Bộ Trạm Hàn Makita C11",
                SeoKeyworks = "Bộ Trạm Hàn Makita C11",
                SeoDescription = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt."
            },
            new Product
            {
                Id = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "Tay Hàn Điều Chỉnh TQ936 80W 200V",
                Slug = "tay-han-dieu-chinh-tq936-80w-200v",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/2e/33/32/2e333249cee6e7f57006ce9aec123f60.jpg",
                SeoTitle = "Tay Hàn Điều Chỉnh TQ936 80W 200V",
                SeoKeyworks = "Tay Hàn Điều Chỉnh TQ936 80W 200V",
                SeoDescription = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt."
            },
            new Product
            {
                Id = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "Bộ Tay Hàn 908S-80W 220V",
                Slug = "bo-tay-han-908s-80w-220v",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/8c/6f/f3/8c6ff345b262a30473228755be595429.jpg",
                SeoTitle = "Bộ Tay Hàn 908S-80W 220V",
                SeoKeyworks = "Bộ Tay Hàn 908S-80W 220V",
                SeoDescription = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt."

            },
            new Product
            {
                Id = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Hiện Diện HLK",
                Slug = "cam-bien-hien-dien-hlk",
                Description = "Tính năng: Phát hiện chuyển động của con người hoặc vật thể trong phạm vi cảm biến. Dễ dàng tích hợp vào các hệ thống điện tử và nhà thông minh",
                ImageUrl = "https://i.pinimg.com/564x/d1/60/83/d16083b9db71c03e103d78f9e9d215eb.jpg",
                SeoTitle = "Cảm Biến Hiện Diện HLK",
                SeoKeyworks = "Cảm Biến Hiện Diện HLK",
                SeoDescription = "Tính năng: Phát hiện chuyển động của con người hoặc vật thể trong phạm vi cảm biến. Dễ dàng tích hợp vào các hệ thống điện tử và nhà thông minh"

            },
            new Product
            {
                Id = new Guid("07acf5bd-e13d-4667-ba8e-70be6785f655"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Rung HDX",
                Slug = "cam-bien-rung-hdx",
                Description = "Tính năng: Giám sát độ rung, sử dụng để đảm bảo an toàn và ổn định cho các thiết bị hoặc công trình quan trọng",
                ImageUrl = "https://i.pinimg.com/564x/ec/3c/d4/ec3cd4cbb811deebd3db2fbbb67fd932.jpg",
                SeoTitle = "Cảm Biến Rung HDX",
                SeoKeyworks = "Cảm Biến Rung HDX",
                SeoDescription = "Tính năng: Giám sát độ rung, sử dụng để đảm bảo an toàn và ổn định cho các thiết bị hoặc công trình quan trọng"
            },
            new Product
            {
                Id = new Guid("00cab8fd-ad0e-433b-8bb0-2c9596809b7b"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Siêu Âm SRF",
                Slug = "cam-bien-sieu-am-srf",
                Description = "Dùng để đo khoảng cách đến các vật thể mà không cần tiếp xúc. Có thể sử dụng để theo dõi vị trí của các vật thể hoặc robot di động hoặc được tích hợp vào các robot để phát hiện và tránh vật cản khi di chuyển",
                ImageUrl = "https://i.pinimg.com/564x/f6/38/09/f63809ff0bb62c49c6f11e3cd89cc16a.jpg",
                SeoTitle = "Cảm Biến Siêu Âm SRF",
                SeoKeyworks = "Cảm Biến Siêu Âm SRF",
                SeoDescription = "Dùng để đo khoảng cách đến các vật thể mà không cần tiếp xúc."
            }
        );

            modelBuilder.Entity<ProductVariant>().HasData(
               new ProductVariant
               {
                   ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                   ProductTypeId = new Guid("ff58b31e-53d5-4216-84a4-0566cb2e9b52"),
                   Price = 450000,
                   OriginalPrice = 400000
               },
               new ProductVariant
               {
                   ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                   ProductTypeId = new Guid("b28481c3-51fb-4a94-8558-0ac0ebfb1607"),
                   Price = 350000
               },
               new ProductVariant
               {
                   ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                   ProductTypeId = new Guid("43a56b31-2f02-4e9d-88b0-2b3ced2276ba"),
                   Price = 200000,
                   OriginalPrice = 250000
               },
               new ProductVariant
               {
                   ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                   ProductTypeId = new Guid("ff58b31e-53d5-4216-84a4-0566cb2e9b52"),
                   Price = 130000
               },
               new ProductVariant
               {
                   ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                   ProductTypeId = new Guid("b28481c3-51fb-4a94-8558-0ac0ebfb1607"),
                   Price = 230000,
                   OriginalPrice = 250000
               },
               new ProductVariant
               {
                   ProductId = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                   ProductTypeId = new Guid("b28481c3-51fb-4a94-8558-0ac0ebfb1607"),
                   Price = 290000
               },
               new ProductVariant
               {
                   ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                   ProductTypeId = new Guid("dfd52a60-8ccf-4ac2-980c-14ddf41e9a18"),
                   Price = 390000
               },
               new ProductVariant
               {
                   ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                   ProductTypeId = new Guid("fbaeaba4-a5bc-487e-b0e5-0967ff543d5d"),
                   Price = 790000
               },
               new ProductVariant
               {
                   ProductId = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                   ProductTypeId = new Guid("dfd52a60-8ccf-4ac2-980c-14ddf41e9a18"),
                   Price = 89000
               },
               new ProductVariant
               {
                   ProductId = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                   ProductTypeId = new Guid("dfd52a60-8ccf-4ac2-980c-14ddf41e9a18"),
                   Price = 229000
               },
               new ProductVariant
               {
                   ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                   ProductTypeId = new Guid("e2d9219f-c57b-4c59-bea0-6a3af2e655a5"),
                   Price = 149000,
                   OriginalPrice = 199000
               },
               new ProductVariant
               {
                   ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                   ProductTypeId = new Guid("f728c3bf-b4d8-41f6-93ab-7a91cba390be"),
                   Price = 229000
               },
               new ProductVariant
               {
                   ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                   ProductTypeId = new Guid("934902b5-c2a2-4fbc-97c2-8887ed45d08e"),
                   Price = 249000,
                   OriginalPrice = 349000
               },
               new ProductVariant
               {
                   ProductId = new Guid("07acf5bd-e13d-4667-ba8e-70be6785f655"),
                   ProductTypeId = new Guid("e2d9219f-c57b-4c59-bea0-6a3af2e655a5"),
                   Price = 99000,
                   OriginalPrice = 149000,
               },
               new ProductVariant
               {
                   ProductId = new Guid("00cab8fd-ad0e-433b-8bb0-2c9596809b7b"),
                   ProductTypeId = new Guid("e2d9219f-c57b-4c59-bea0-6a3af2e655a5"),
                   Price = 149000
               }
        );
            modelBuilder.Entity<ProductValue>().HasData(
                  new ProductValue
                  {
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ProductAttributeId = new Guid("bd2d16dc-10a9-40b1-b76a-e39f3c015086"),
                      Value = "ATmega2560"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ProductAttributeId = new Guid("09ec0726-d537-4c92-aaaf-760f19c6999f"),
                      Value = "5V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ProductAttributeId = new Guid("d369dc76-92cf-417a-aea5-17616c87d4ce"),
                      Value = "7-12V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ProductAttributeId = new Guid("50b7176f-4b13-484b-aec4-edf9383b9232"),
                      Value = "6-20V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                      ProductAttributeId = new Guid("bd2d16dc-10a9-40b1-b76a-e39f3c015086"),
                      Value = "ATmega328P"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                      ProductAttributeId = new Guid("09ec0726-d537-4c92-aaaf-760f19c6999f"),
                      Value = "5V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                      ProductAttributeId = new Guid("d369dc76-92cf-417a-aea5-17616c87d4ce"),
                      Value = "7-12V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                      ProductAttributeId = new Guid("50b7176f-4b13-484b-aec4-edf9383b9232"),
                      Value = "6-20V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                      ProductAttributeId = new Guid("bd2d16dc-10a9-40b1-b76a-e39f3c015086"),
                      Value = "ESP32-D0WDQ6"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                      ProductAttributeId = new Guid("09ec0726-d537-4c92-aaaf-760f19c6999f"),
                      Value = "3.3V"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                      ProductAttributeId = new Guid("b96ab5d0-3155-4688-8fb4-c6427e0661d5"),
                      Value = "50-60W"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                      ProductAttributeId = new Guid("99bf3d94-a248-46f8-bbcb-0f9ae07ce1af"),
                      Value = "200°C đến 480°C"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                      ProductAttributeId = new Guid("a5913406-23e7-4451-a19c-242c974e312e"),
                      Value = "LCD"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                      ProductAttributeId = new Guid("b96ab5d0-3155-4688-8fb4-c6427e0661d5"),
                      Value = "50W-60W"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                      ProductAttributeId = new Guid("99bf3d94-a248-46f8-bbcb-0f9ae07ce1af"),
                      Value = "200°C đến 480°C"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                      ProductAttributeId = new Guid("a5913406-23e7-4451-a19c-242c974e312e"),
                      Value = "LED"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                      ProductAttributeId = new Guid("b96ab5d0-3155-4688-8fb4-c6427e0661d5"),
                      Value = "50W-60W"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                      ProductAttributeId = new Guid("99bf3d94-a248-46f8-bbcb-0f9ae07ce1af"),
                      Value = "90°C đến 480°C"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                      ProductAttributeId = new Guid("a5913406-23e7-4451-a19c-242c974e312e"),
                      Value = " LED"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                      ProductAttributeId = new Guid("c84ec2fc-651b-42e1-b073-022596ac90c0"),
                      Value = "PIR (Passive Infrared) "
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                      ProductAttributeId = new Guid("4150124b-2a58-4d98-8abe-f380e99a6fa9"),
                      Value = "110° đến 180°"
                  },
                  new ProductValue
                  {
                      ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                      ProductAttributeId = new Guid("09ec0726-d537-4c92-aaaf-760f19c6999f"),
                      Value = "5V DC đến 24V DC"
                  }
        );
            modelBuilder.Entity<ProductImage>().HasData(
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ImageUrl = "https://i.pinimg.com/564x/28/3d/b1/283db15665e274cb4e6d83741238e2ae.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("318f6a20-3c0b-40ca-9cf0-9533e83d3734"),
                      ImageUrl = "https://i.pinimg.com/564x/75/cb/75/75cb751bad06d603883cd3a92190cbf6.jpg",
                      IsMain = false,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                      ImageUrl = "https://i.pinimg.com/564x/19/3a/65/193a65211060c29ffbddac99ea01ac59.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                      ImageUrl = "https://i.pinimg.com/564x/47/68/73/476873b27d117bb46f76f1bb50b73499.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                      ImageUrl = "https://i.pinimg.com/564x/22/db/f1/22dbf1e839f495516eba3842d35c7948.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                      ImageUrl = "https://i.pinimg.com/564x/2e/33/32/2e333249cee6e7f57006ce9aec123f60.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                      ImageUrl = "https://i.pinimg.com/564x/8c/6f/f3/8c6ff345b262a30473228755be595429.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("00cab8fd-ad0e-433b-8bb0-2c9596809b7b"),
                      ImageUrl = "https://i.pinimg.com/564x/f6/38/09/f63809ff0bb62c49c6f11e3cd89cc16a.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                      ImageUrl = "https://i.pinimg.com/564x/d1/60/83/d16083b9db71c03e103d78f9e9d215eb.jpg",
                      IsMain = true,
                  },
                  new ProductImage
                  {
                      Id = Guid.NewGuid(),
                      ProductId = new Guid("07acf5bd-e13d-4667-ba8e-70be6785f655"),
                      ImageUrl = "https://i.pinimg.com/564x/ec/3c/d4/ec3cd4cbb811deebd3db2fbbb67fd932.jpg",
                      IsMain = true,
                  }
                );
            modelBuilder.Entity<BlogEntity>().HasData(
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "Hướng dẫn chọn mua linh kiện điện tử cho người mới bắt đầu",
                      Slug = "huong-dan-chon-mua-linh-kien-dien-tu-cho-nguoi-moi-bat-dau",
                      Image = "https://i.pinimg.com/564x/35/27/9d/35279d8e4578af98dc16bf7cb1d633ee.jpg",
                      Description = "Bài viết này sẽ giúp bạn hiểu rõ hơn về cách chọn mua linh kiện điện tử phù hợp cho các dự án của mình.",
                      Content = "<p>Chọn mua linh kiện điện tử có thể là một thử thách đối với người mới bắt đầu. Dưới đây là một số mẹo giúp bạn:</p><ol><li>Xác định nhu cầu sử dụng.</li><li>Nghiên cứu các loại linh kiện phổ biến.</li><li>Mua sắm từ các nhà cung cấp uy tín.</li><li>Kiểm tra chất lượng và bảo hành sản phẩm.</li><li>Tham khảo ý kiến từ cộng đồng và chuyên gia.</li></ol><p>Hy vọng bạn sẽ chọn được những linh kiện phù hợp cho dự án của mình!</p>",
                      SeoTitle = "Hướng dẫn chọn mua linh kiện điện tử - Mẹo cho người mới bắt đầu",
                      SeoDescription = "Hướng dẫn chi tiết cách chọn mua linh kiện điện tử phù hợp cho người mới bắt đầu.",
                      SeoKeyworks = "chọn mua linh kiện điện tử, hướng dẫn chọn linh kiện, linh kiện điện tử"
                  },
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "10 ứng dụng phổ biến của PLC trong công nghiệp",
                      Slug = "10-ung-dung-pho-bien-cua-plc-trong-cong-nghiep",
                      Image = "https://i.pinimg.com/736x/7d/e0/8c/7de08ce31b70785e7e299f25146b4a28.jpg",
                      Description = "Khám phá 10 ứng dụng phổ biến của PLC trong các hệ thống công nghiệp hiện đại.",
                      Content = "<p>PLC (Programmable Logic Controller) là một thiết bị không thể thiếu trong tự động hóa công nghiệp. Dưới đây là 10 ứng dụng phổ biến của PLC:</p><ul><li>Điều khiển băng tải.</li><li>Quản lý hệ thống đèn giao thông.</li><li>Điều khiển máy móc sản xuất.</li><li>Giám sát và điều khiển hệ thống bơm.</li><li>Quản lý hệ thống điện năng.</li><li>Điều khiển hệ thống HVAC.</li><li>Điều khiển dây chuyền đóng gói.</li><li>Quản lý hệ thống nước thải.</li><li>Điều khiển hệ thống tưới tiêu tự động.</li><li>Giám sát và điều khiển các hệ thống an ninh.</li></ul><p>PLC giúp nâng cao hiệu quả và độ chính xác trong các quy trình công nghiệp.</p>",
                      SeoTitle = "10 ứng dụng phổ biến của PLC trong công nghiệp - Tự động hóa công nghiệp",
                      SeoDescription = "Khám phá 10 ứng dụng phổ biến của PLC trong các hệ thống công nghiệp hiện đại.",
                      SeoKeyworks = "PLC, ứng dụng PLC, tự động hóa công nghiệp"
                  },
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "Lợi ích của việc sử dụng linh kiện điện tử chất lượng cao",
                      Slug = "loi-ich-cua-viec-su-dung-linh-kien-dien-tu-chat-luong-cao",
                      Image = "https://i.pinimg.com/736x/37/2c/56/372c56593de74d024efb29427ef2e449.jpg",
                      Description = "Tìm hiểu những lợi ích khi sử dụng linh kiện điện tử chất lượng cao cho các dự án của bạn.",
                      Content = "<p>Sử dụng linh kiện điện tử chất lượng cao mang lại nhiều lợi ích cho các dự án của bạn. Dưới đây là một số lợi ích:</p><ul><li>Độ bền và tuổi thọ cao.</li><li>Hiệu suất hoạt động tốt hơn.</li><li>Giảm thiểu rủi ro hỏng hóc.</li><li>Tối ưu hóa chi phí bảo trì.</li><li>Đảm bảo an toàn cho người sử dụng.</li></ul><p>Hãy luôn chọn mua linh kiện điện tử từ những nhà cung cấp uy tín để đảm bảo chất lượng.</p>",
                      SeoTitle = "Lợi ích của việc sử dụng linh kiện điện tử chất lượng cao - Đảm bảo hiệu suất và an toàn",
                      SeoDescription = "Tìm hiểu những lợi ích khi sử dụng linh kiện điện tử chất lượng cao cho các dự án của bạn.",
                      SeoKeyworks = "linh kiện điện tử chất lượng cao, lợi ích của linh kiện chất lượng, hiệu suất linh kiện điện tử"
                  }
                );
            modelBuilder.Entity<DiscountEntity>().HasData(
                 new DiscountEntity
                 {
                     Id = Guid.NewGuid(),
                     Code = "SUMMER24",
                     VoucherName = "Summer 24% Discount",
                     IsDiscountPercent = true,
                     DiscountValue = 24.00,
                     MinOrderCondition = 200000,
                     MaxDiscountValue = 80000,
                     Quantity = 1000,
                     StartDate = DateTime.Now,
                     EndDate = DateTime.Now.AddDays(30),
                 },
                  new DiscountEntity
                  {
                      Id = Guid.NewGuid(),
                      Code = "SUMMER20000",
                      VoucherName = "Summer 20000VND Discount",
                      IsDiscountPercent = false,
                      DiscountValue = 20000,
                      MinOrderCondition = 100000,
                      MaxDiscountValue = 0,
                      Quantity = 1000,
                      StartDate = DateTime.Now,
                      EndDate = DateTime.Now.AddDays(30),
                  }
               );
        }

    }
}
