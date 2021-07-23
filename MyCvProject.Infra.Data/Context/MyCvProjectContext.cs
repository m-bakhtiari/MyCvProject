using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.Entities.Order;
using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using System.Linq;
using MyCvProject.Core.Generator;
using MyCvProject.Core.Security;

namespace MyCvProject.Infra.Data.Context
{
    public class MyCvProjectContext : DbContext
    {

        public MyCvProjectContext(DbContextOptions<MyCvProjectContext> options) : base(options)
        {

        }


        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDelete);

            #region Add Primary Data For Role

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                RoleId = Domain.Consts.Const.RoleIdForUser,
                IsDelete = false,
                RoleTitle = "کاربر"
            }, new Role()
            {
                RoleId = Domain.Consts.Const.RoleIdForAdmin,
                IsDelete = false,
                RoleTitle = "مدیر"
            }, new Role()
            {
                RoleId = Domain.Consts.Const.RoleIdForTeacher,
                IsDelete = false,
                RoleTitle = "استاد"
            });

            #endregion

            #region Add Primary Data For Permission

            modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForAdmin,
                PermissionTitle = "پنل مدیریت",
                ParentID = null
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForManageUser,
                PermissionTitle = "مدیریت کاربران",
                ParentID = Domain.Consts.Const.PermissionIdForAdmin
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForCreateUser,
                PermissionTitle = "افزودن کاربران",
                ParentID = Domain.Consts.Const.PermissionIdForManageUser
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForEditUser,
                PermissionTitle = "ویرایش کاربران",
                ParentID = Domain.Consts.Const.PermissionIdForManageUser
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForDeleteUser,
                PermissionTitle = "حذف کاربران",
                ParentID = Domain.Consts.Const.PermissionIdForManageUser
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForManageRole,
                PermissionTitle = "مدیریت نقش ها",
                ParentID = Domain.Consts.Const.PermissionIdForAdmin
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForCreateRole,
                PermissionTitle = "افزودن نقش ها",
                ParentID = Domain.Consts.Const.PermissionIdForManageRole
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForEditRole,
                PermissionTitle = "ویرایش نقش ها",
                ParentID = Domain.Consts.Const.PermissionIdForManageRole
            }, new Permission()
            {
                PermissionId = Domain.Consts.Const.PermissionIdForDeleteRole,
                PermissionTitle = "حذف نقش ها",
                ParentID = Domain.Consts.Const.PermissionIdForManageRole
            });

            #endregion

            #region Add User For Admin With All Permissions

            modelBuilder.Entity<User>().HasData(new User()
            {
                UserId = 1,
                UserName = "admin",
                Email = "admin@gmail.com",
                IsDelete = false,
                IsActive = true,
                UserAvatar = Domain.Consts.Const.DefaultUserAvatar,
                RegisterDate = DateTime.Now,
                Password = PasswordHelper.EncodePasswordMd5("123"),
                ActiveCode = NameGenerator.GenerateUniqCode(),
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                RoleId = Domain.Consts.Const.RoleIdForAdmin,
                UserId = 1,
                UR_Id = 1,
            });

            modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                RoleId = Domain.Consts.Const.RoleIdForAdmin,
                PermissionId = Domain.Consts.Const.PermissionIdForAdmin,
                RP_Id = 1
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
