using AppData.Enum;
using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Initialization
{
  public class Seeding
    {
        public static void SeedingAccount(ModelBuilder modelBuilder)
        {
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash1, out byte[] passwordSalt1);
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash2, out byte[] passwordSalt2);
            CryptographyHelper.CreatePasswordHash("123456", out byte[] passwordHash3, out byte[] passwordSalt3);

            modelBuilder.Entity<AccountEntity>().HasData(
                   new AccountEntity
                   {
                       Id = Guid.NewGuid(),
                       Username = "admin@example.com",
                       PasswordHash = passwordHash1,
                       PasswordSalt = passwordSalt1,
                       Role = UserTypeEnum.Admin
                   },
                   new AccountEntity
                   {
                       Id = new Guid("2b25a754-a50e-4468-942c-d65c0bc2c86f"),
                       Username = "customer@example.com",
                       PasswordHash = passwordHash2,
                       PasswordSalt = passwordSalt2,
                       Role = UserTypeEnum.Customer,
                       Name = "John Doe",
                       Email = "johndoe@example.com",
                       PhoneNumber = "0123456789",
                   },
                   new AccountEntity
                   {
                       Id = Guid.NewGuid(),
                       Username = "employee@example.com",
                       PasswordHash = passwordHash3,
                       PasswordSalt = passwordSalt3,
                       Role = UserTypeEnum.Employee,
                   }
                 );

            modelBuilder.Entity<AddressEntity>().HasData(
                   new AddressEntity
                   {
                       Id = Guid.NewGuid(),
                       IdAccount = new Guid("2b25a754-a50e-4468-942c-d65c0bc2c86f"),
                       Country = "Vietnam",
                       City = "Hanoi",
                       District = "Cau Giay",
                       HomeAddress = "123 Main Street",
                       Status = 1
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
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f")
            },
            new Product
            {
                Id = new Guid("ce50c69d-5897-4e3d-8d2d-081114ed1fb0"),
                Title = "Kit Arduino UNO R3",
                Slug = "kit-arduino-uno-r3",
                Description = "Tính năng: Tương thích với nhiều loại shield (bảng mở rộng) và phụ kiện, Đa dạng chân I/O( với 14 chân Digital I/O (bao gồm 6 chân PWM) và 6 chân Analog Input), 32KB bộ nhớ Flash, 2 KB SRAM và 1 KB EEPROM",
                ImageUrl = "https://i.pinimg.com/564x/19/3a/65/193a65211060c29ffbddac99ea01ac59.jpg",
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f")
            },
            new Product
            {
                Id = new Guid("c7537965-c2cb-4e77-bfc5-6c466c9a3bea"),
                Title = "Kit Arduino WiFi ESP-32",
                Slug = "kit-arduino-wifi-esp-32",
                Description = "Tính năng: Hỗ trợ Wi-Fi 802.11 b/g/n và Bluetooth v4.2, Hiệu suất cao( với bộ xử lý dual-core 32-bit Xtensa LX6, hoạt động ở tần số lên đến 240 MH), Bộ nhớ lớn( với 520 KB SRAM và tùy chọn bộ nhớ Flash từ 4MB trở lên)",
                ImageUrl = "https://i.pinimg.com/564x/47/68/73/476873b27d117bb46f76f1bb50b73499.jpg",
                CategoryId = new Guid("a186203e-0d11-4c22-a45e-58ecfeed368f")
            },
            new Product
            {
                Id = new Guid("4f5c260c-0870-4940-a394-b20c56b3fcca"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "“Bộ Trạm Hàn Makita C11",
                Slug = "bo-tram-han-makita-c11",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/22/db/f1/22dbf1e839f495516eba3842d35c7948.jpg",
            },
            new Product
            {
                Id = new Guid("f2b7ac53-e3e5-4f7c-8094-99530bbde9eb"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "Tay Hàn Điều Chỉnh TQ936 80W 200V",
                Slug = "tay-han-dieu-chinh-tq936-80w-200v",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/2e/33/32/2e333249cee6e7f57006ce9aec123f60.jpg",
            },
            new Product
            {
                Id = new Guid("321ec52d-5fb6-4b1b-bb35-6f73cf92396d"),
                CategoryId = new Guid("2c8eb836-090b-4a18-a869-620d7f527180"),
                Title = "Bộ Tay Hàn 908S-80W 220V",
                Slug = "bo-tay-han-908s-80w-220v",
                Description = "Tính năng: Tốc độ gia nhiệt nhanh, chế độ ngủ tự động, thiết kế chống tĩnh điện và bảo vệ quá nhiệt.",
                ImageUrl = "https://i.pinimg.com/564x/8c/6f/f3/8c6ff345b262a30473228755be595429.jpg",

            },
            new Product
            {
                Id = new Guid("106e97ab-bbce-44b8-95c4-a287752d8561"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Hiện Diện HLK",
                Slug = "cam-bien-hien-dien-hlk",
                Description = "Tính năng: Phát hiện chuyển động của con người hoặc vật thể trong phạm vi cảm biến. Dễ dàng tích hợp vào các hệ thống điện tử và nhà thông minh",
                ImageUrl = "https://i.pinimg.com/564x/d1/60/83/d16083b9db71c03e103d78f9e9d215eb.jpg",

            },
            new Product
            {
                Id = new Guid("07acf5bd-e13d-4667-ba8e-70be6785f655"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Rung HDX",
                Slug = "cam-bien-rung-hdx",
                Description = "Tính năng: Giám sát độ rung, sử dụng để đảm bảo an toàn và ổn định cho các thiết bị hoặc công trình quan trọng",
                ImageUrl = "https://i.pinimg.com/564x/ec/3c/d4/ec3cd4cbb811deebd3db2fbbb67fd932.jpg",
            },
            new Product
            {
                Id = new Guid("00cab8fd-ad0e-433b-8bb0-2c9596809b7b"),
                CategoryId = new Guid("c236f9f6-2c4c-4ba3-99ed-9cf81ee9bf46"),
                Title = "Cảm Biến Siêu Âm SRF",
                Slug = "cam-bien-sieu-am-srf",
                Description = "Dùng để đo khoảng cách đến các vật thể mà không cần tiếp xúc. Có thể sử dụng để theo dõi vị trí của các vật thể hoặc robot di động hoặc được tích hợp vào các robot để phát hiện và tránh vật cản khi di chuyển",
                ImageUrl = "https://i.pinimg.com/564x/f6/38/09/f63809ff0bb62c49c6f11e3cd89cc16a.jpg"
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
                      ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png",
                      IsMain = true,
                  }
                );
            modelBuilder.Entity<BlogEntity>().HasData(
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "Cách trồng rau sạch tại nhà",
                      Slug = "cach-trong-rau-sach-tai-nha",
                      Image = "https://i.pinimg.com/564x/c1/e1/5a/c1e15a598e8781a59fffe859ddf66595.jpg",
                      Description = "Hướng dẫn chi tiết cách trồng rau sạch tại nhà cho người mới bắt đầu.",
                      Content = "<p>Trồng rau sạch tại nhà đang trở thành xu hướng. Bạn có thể tự tay trồng rau sạch để đảm bảo an toàn thực phẩm cho gia đình mình. <strong>Hãy bắt đầu với những bước đơn giản sau:</strong></p><ol><li>Chọn loại rau phù hợp.</li><li>Chuẩn bị đất và chậu trồng.</li><li>Gieo hạt và chăm sóc cây con.</li><li>Thu hoạch và sử dụng.</li></ol><p>Chúc bạn thành công!</p>",
                      SeoTitle = "Cách trồng rau sạch tại nhà - Hướng dẫn chi tiết",
                      SeoDescription = "Hướng dẫn chi tiết cách trồng rau sạch tại nhà cho người mới bắt đầu.",
                      SeoKeyworks = "trồng rau sạch, rau sạch tại nhà, hướng dẫn trồng rau"
                  },
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "10 món ăn ngon từ thịt gà",
                      Slug = "10-mon-an-ngon-tu-thit-ga",
                      Image = "https://i.pinimg.com/564x/f6/5d/23/f65d23606ba71e48cc7f6c0b52f44b29.jpg",
                      Description = "Khám phá 10 món ăn ngon từ thịt gà mà bạn không thể bỏ qua.",
                      Content = "<p>Thịt gà là nguyên liệu dễ chế biến và rất phổ biến trong bữa ăn hàng ngày. Dưới đây là <strong>10 món ăn ngon từ thịt gà</strong> bạn có thể thử:</p><ul><li>Gà nướng mật ong.</li><li>Gà chiên xù.</li><li>Gà xào sả ớt.</li><li>Canh gà nấu nấm.</li><li>Gà luộc chấm muối tiêu.</li><li>Gỏi gà xé phay.</li><li>Cơm gà Hải Nam.</li><li>Gà kho gừng.</li><li>Gà hấp lá chanh.</li><li>Gà rang muối.</li></ul><p>Hãy thử và cảm nhận hương vị đặc biệt của từng món ăn!</p>",
                      SeoTitle = "10 món ăn ngon từ thịt gà - Bí quyết nấu ăn",
                      SeoDescription = "Khám phá 10 món ăn ngon từ thịt gà mà bạn không thể bỏ qua.",
                      SeoKeyworks = "món ăn từ thịt gà, nấu ăn, bí quyết nấu ăn"
                  },
                  new BlogEntity
                  {
                      Id = Guid.NewGuid(),
                      Title = "Lợi ích của việc đọc sách mỗi ngày",
                      Slug = "loi-ich-cua-viec-doc-sach-moi-ngay",
                      Image = "https://i.pinimg.com/564x/66/dc/ca/66dcca5a43bc51a2d669fa4782618c12.jpg",
                      Description = "Tìm hiểu những lợi ích tuyệt vời của việc đọc sách mỗi ngày.",
                      Content = "<p>Đọc sách mỗi ngày mang lại nhiều lợi ích cho sức khỏe tinh thần và kiến thức của bạn. <strong>Dưới đây là một số lợi ích:</strong></p><ul><li>Cải thiện khả năng tập trung.</li><li>Mở rộng vốn từ vựng.</li><li>Giảm căng thẳng và lo âu.</li><li>Tăng cường khả năng phân tích và suy luận.</li><li>Cải thiện trí nhớ.</li></ul><p>Hãy dành ít nhất 30 phút mỗi ngày để đọc sách và cảm nhận sự thay đổi tích cực!</p>",
                      SeoTitle = "Lợi ích của việc đọc sách mỗi ngày - Sức khỏe tinh thần",
                      SeoDescription = "Tìm hiểu những lợi ích tuyệt vời của việc đọc sách mỗi ngày.",
                      SeoKeyworks = "đọc sách, lợi ích của đọc sách, sức khỏe tinh thần"
                  }
                );
        }

    }
}
