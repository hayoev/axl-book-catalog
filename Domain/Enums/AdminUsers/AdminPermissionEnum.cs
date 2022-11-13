using System;
using Domain.Common.BaseEnums;

namespace Domain.Enums.AdminUsers
{
    public class AdminPermissionEnum : BaseEnum<AdminPermissionEnum, Guid>
    {
        #region Books

        public static AdminPermissionEnum BookList { get; } =
            new AdminPermissionEnum(Guid.Parse("3427fa6f-ae49-11eb-acc1-244bfee059a7"), "Список книг");
  
        public static AdminPermissionEnum BookDetail { get; } =
            new AdminPermissionEnum(Guid.Parse("7b8063f7-58ab-464a-97d2-2357f5ffbee4"), "Детальный просмотр книг");

        public static AdminPermissionEnum BookEdit { get; } =
            new AdminPermissionEnum(Guid.Parse("bc1570ad-f8a3-4dc5-b75a-3bf6c83812e7"), "Редактировать книгу");

        public static AdminPermissionEnum BookCreate { get; } =
            new AdminPermissionEnum(Guid.Parse("4aee4be3-c050-4f6c-ac05-039a45f80d31"), "Добавить книгу");
 
        public static AdminPermissionEnum BookDelete { get; } =
            new AdminPermissionEnum(Guid.Parse("4d225cee-95a6-448c-8d9c-d3c0f3d07c33"), "Удалить книгу");

        #endregion 
        
        #region Authors

        public static AdminPermissionEnum AuthorList { get; } =
            new AdminPermissionEnum(Guid.Parse("85b7cfa4-e0fe-47ec-a09a-127d7c87d50a"), "Список авторов");
  
        public static AdminPermissionEnum AuthorDetail { get; } =
            new AdminPermissionEnum(Guid.Parse("14ca8f81-4116-4981-adf0-ae704155031b"), "Детальный просмотр автора");

        public static AdminPermissionEnum AuthorEdit { get; } =
            new AdminPermissionEnum(Guid.Parse("2146d8b3-c349-4d32-acb7-8db2836e48ca"), "Редактировать автора");

        public static AdminPermissionEnum AuthorCreate { get; } =
            new AdminPermissionEnum(Guid.Parse("ca785d61-5b6e-43ce-a7a7-5595ec35cafd"), "Добавить автора");
 
        public static AdminPermissionEnum AuthorDelete { get; } =
            new AdminPermissionEnum(Guid.Parse("79ce1ebb-96b5-455f-b7a8-023cdea081e1"), "Удалить автора");

        #endregion

        
        #region Seed
        public static AdminPermissionEnum SeedStart { get; } =
            new AdminPermissionEnum(Guid.Parse("6ec04cd2-aefd-11eb-acc1-244bfee059a7"), "Data Seed. Запуск автозаполнения данных в БД");

        #endregion
        protected AdminPermissionEnum(Guid val, string name) : base(val, name)
        {
        }
    }
}