﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce.Data;

#nullable disable

namespace e_commerce.Data.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20231021171750_update_tableCategory1")]
    partial class update_tableCategory1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("e_commerce.Model.Entities.CartEntities", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.CategoryEntities", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.OrderDetailEntities", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OderId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("ProductId", "OderId");

                    b.HasIndex("OderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.OrderEntities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.ParentCategoryEntities", b =>
                {
                    b.Property<int>("ParentCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentCategoryId"));

                    b.Property<string>("ParentCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParentCategoryId");

                    b.ToTable("ParentCategories");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.ProductEntities", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int?>("CategoryEntitiesCategoryID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryEntitiesCategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.UserEntities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.UserRoleEntities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.CartEntities", b =>
                {
                    b.HasOne("e_commerce.Model.Entities.ProductEntities", "Product")
                        .WithMany("CartEntities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Cart_Product_PM");

                    b.HasOne("e_commerce.Model.Entities.UserEntities", "User")
                        .WithMany("CartEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Cart_User_PM");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.OrderDetailEntities", b =>
                {
                    b.HasOne("e_commerce.Model.Entities.OrderEntities", "Order")
                        .WithMany("OrderDetailEntities")
                        .HasForeignKey("OderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Order_PM");

                    b.HasOne("e_commerce.Model.Entities.ProductEntities", "Product")
                        .WithMany("OrderDetailEntities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Product_PM");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.OrderEntities", b =>
                {
                    b.HasOne("e_commerce.Model.Entities.UserEntities", "User")
                        .WithMany("OrderEntities")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.ProductEntities", b =>
                {
                    b.HasOne("e_commerce.Model.Entities.CategoryEntities", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryEntitiesCategoryID");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.UserEntities", b =>
                {
                    b.HasOne("e_commerce.Model.Entities.UserRoleEntities", "Role")
                        .WithMany("UserEntities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.CategoryEntities", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.OrderEntities", b =>
                {
                    b.Navigation("OrderDetailEntities");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.ProductEntities", b =>
                {
                    b.Navigation("CartEntities");

                    b.Navigation("OrderDetailEntities");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.UserEntities", b =>
                {
                    b.Navigation("CartEntities");

                    b.Navigation("OrderEntities");
                });

            modelBuilder.Entity("e_commerce.Model.Entities.UserRoleEntities", b =>
                {
                    b.Navigation("UserEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
