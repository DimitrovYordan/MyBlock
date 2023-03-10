// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBlock.Data;

namespace MyBlock.Migrations
{
    [DbContext(typeof(MyBlockContext))]
    partial class MyBlockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyBlock.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCommentID")
                        .HasColumnType("int");

                    b.Property<int>("ParentPostID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimePosted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ParentCommentID");

                    b.HasIndex("ParentPostID");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorID = 1,
                            Content = "Tova e 1 komentar",
                            ParentPostID = 1,
                            TimePosted = new DateTime(2023, 1, 14, 10, 45, 22, 522, DateTimeKind.Local).AddTicks(450)
                        },
                        new
                        {
                            Id = 2,
                            AuthorID = 2,
                            Content = "Tova e 2 komentar",
                            ParentPostID = 2,
                            TimePosted = new DateTime(2023, 1, 14, 10, 45, 22, 525, DateTimeKind.Local).AddTicks(8105)
                        });
                });

            modelBuilder.Entity("MyBlock.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(8192)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorID");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorID = 1,
                            Content = "Siguren li si?",
                            Rating = 0,
                            TimePosted = new DateTime(2023, 1, 14, 10, 45, 22, 526, DateTimeKind.Local).AddTicks(500),
                            Title = "Purvi post"
                        },
                        new
                        {
                            Id = 2,
                            AuthorID = 2,
                            Content = "Nqma da stane?",
                            Rating = 0,
                            TimePosted = new DateTime(2023, 1, 14, 10, 45, 22, 526, DateTimeKind.Local).AddTicks(1303),
                            Title = "Vtori post"
                        });
                });

            modelBuilder.Entity("MyBlock.Models.Relational_Classes.DislikedByUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("DislikedByUsers");
                });

            modelBuilder.Entity("MyBlock.Models.Relational_Classes.LikedByUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("LikedByUser");
                });

            modelBuilder.Entity("MyBlock.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "pesho@gmail.com",
                            FirstName = "Pesho",
                            IsAdmin = true,
                            LastName = "Peshov",
                            Password = "pass123",
                            Username = "PeshoP"
                        },
                        new
                        {
                            Id = 2,
                            Email = "gosho@gmail.com",
                            FirstName = "Gosho",
                            IsAdmin = false,
                            LastName = "Goshov",
                            Password = "pass123",
                            Username = "GoshoG"
                        });
                });

            modelBuilder.Entity("MyBlock.Models.Comment", b =>
                {
                    b.HasOne("MyBlock.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("MyBlock.Models.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentID");

                    b.HasOne("MyBlock.Models.Post", "ParentPost")
                        .WithMany("Comments")
                        .HasForeignKey("ParentPostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ParentComment");

                    b.Navigation("ParentPost");
                });

            modelBuilder.Entity("MyBlock.Models.Post", b =>
                {
                    b.HasOne("MyBlock.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("MyBlock.Models.Relational_Classes.DislikedByUser", b =>
                {
                    b.HasOne("MyBlock.Models.Post", "Post")
                        .WithMany("DislikedByUsers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBlock.Models.User", "User")
                        .WithMany("DislikedPosts")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBlock.Models.Relational_Classes.LikedByUser", b =>
                {
                    b.HasOne("MyBlock.Models.Post", "Post")
                        .WithMany("LikedByUsers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBlock.Models.User", "User")
                        .WithMany("LikedPosts")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBlock.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DislikedByUsers");

                    b.Navigation("LikedByUsers");
                });

            modelBuilder.Entity("MyBlock.Models.User", b =>
                {
                    b.Navigation("DislikedPosts");

                    b.Navigation("LikedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
