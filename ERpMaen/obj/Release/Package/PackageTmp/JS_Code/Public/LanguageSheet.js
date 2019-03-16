//get coocki name 
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

//var currentLanguage = getCookie("Language");
var currentLanguage = "Arabic";
allLanguage = [
      {
              "english": "Listing",
              "arabic": "العقارات"
      },
      {
                "english": "Work",
                "arabic": "المهام"
      },
     {
         "english": "Advertise",
         "arabic": "النشر"
     },
     {
          "english": "Utilities",
          "arabic": "الخدمات"
         },
{
    "english": "Statistics",
         "arabic": "الاحصائيات"
},
{
    "english": "import",
    "arabic": "استيراد"
},
{
    "english": "templates",
    "arabic": "نماذج"
},
{
    "english": "general",
    "arabic": "عام"
},
    {
        "english": "Website Settings",
        "arabic": "اعدادات موقع الشركة"
    },
      {
          "english": "About Us",
          "arabic": "عن الشركة"
      },
      {
          "english": "Type",
          "arabic": "النوع"
      },
      {
          "english": "Select",
          "arabic": "اختر"
      },
      {
          "english": "Our Story",
          "arabic": "قصتنا"
      },
      {
          "english": "Vission & Mission",
          "arabic": "الرؤية و الهدف"
      },
      {
          "english": "Ar Title",
          "arabic": "العنوان بالعربية"
      },
      {
          "english": "En Title",
          "arabic": "العنوان بالانكليزية"
      },
      {
          "english": "Ar Description ",
          "arabic": "الوصف بالعربية"
      },
      {
          "english": "En Description",
          "arabic": "الوصف بالانكليزية"
      },
      {
          "english": "Image",
          "arabic": "صورة"
      },
      {
          "english": "Projects ",
          "arabic": "المشاريع"
      },
      {
          "english": "Status",
          "arabic": "الحالة"
      },
      {
          "english": "Select",
          "arabic": "اختر"
      },
      {
          "english": "Current",
          "arabic": "حالى"
      },
      {
          "english": "Old",
          "arabic": "قديم"
      },
      {
          "english": "Ar Title",
          "arabic": "العنوان بالعربىة"
      },
      {
          "english": "En Title",
          "arabic": "العنوان بالانكليزية"
      },
      {
          "english": "Ar Description ",
          "arabic": "الوصف بالعربية"
      },
      {
          "english": "En Description",
          "arabic": "الوصف بالانكليزية"
      },
      {
          "english": "Image",
          "arabic": "صورة"
      },
      {
          "english": "Managers",
          "arabic": "المديرين"
      },
      {
          "english": "Ar Name",
          "arabic": "الاسم بالعربية"
      },
      {
          "english": "En Name",
          "arabic": "الاسم بالانكليزية"
      },
      {
          "english": "Ar Job",
          "arabic": "المهنة بالعربية"
      },
      {
          "english": "En Job",
          "arabic": "المهنة بالانكليزية"
      },
      {
          "english": "Type",
          "arabic": "النوع"
      },
      {
          "english": "Ar Description ",
          "arabic": "الوصف بالعربية"
      },
      {
          "english": "En Description",
          "arabic": "الوصف بالانكليزية"
      },
      {
          "english": "Image",
          "arabic": "صورة"
      },
      {
          "english": "News",
          "arabic": "الاخبار"
      },
      {
          "english": "Ar Title",
          "arabic": "العنوان بالعربىة"
      },
      {
          "english": "En Title",
          "arabic": "العنوان بالانكليزية"
      },
      {
          "english": "Ar Description ",
          "arabic": "الوصف بالعربية"
      },
      {
          "english": "En Description",
          "arabic": "الوصف بالانكليزية"
      },
      {
          "english": "Image",
          "arabic": "صورة"
      },
      {
          "english": "Date",
          "arabic": "التاريخ"
      },
      {
          "english": "Annual Report",
          "arabic": "التقرير السنوى"
      },
      {
          "english": "Main Type",
          "arabic": "النوع الرئيسى"
      },
      {
          "english": "Financial Statements",
          "arabic": "البيانات المالية"
      },
      {
          "english": "Annual Report",
          "arabic": "التقرير السنوى"
      },
      {
          "english": "Sub Type",
          "arabic": "النوع الفرعى"
      },
      {
          "english": "First Quarter",
          "arabic": "ألربع الاول"
      },
      {
          "english": "Half year",
          "arabic": "النصف سنوى"
      },
      {
          "english": "Nine Months ",
          "arabic": "تسعة اشهر "
      },
      {
          "english": "Title",
          "arabic": "العنوان"
      },
      {
          "english": "Description",
          "arabic": "الوصف"
      },
      {
          "english": "Files",
          "arabic": "الملفات"
      },
      {
          "english": "Sectors",
          "arabic": "القطاعات"
      },
      {
          "english": "Ar Name",
          "arabic": "الاسم بالعربية"
      },
      {
          "english": "En Name",
          "arabic": "الاسم بالانكليزية"
      },
      {
          "english": "Managments ",
          "arabic": "الادارات"
      },
      {
          "english": "Sector",
          "arabic": "القطاع"
      },
      {
          "english": "Management Ar Name",
          "arabic": "اسم الادارة بالعربية"
      },
      {
          "english": "Management En Name",
          "arabic": "اسم الادارة بالانكليزية"
      },
      {
          "english": "Management Ar Description",
          "arabic": "وصف الادارة بالعربية"
      },
      {
          "english": "Management En Description",
          "arabic": "وصف الادارة بالانكليزية "
      },
      {
          "english": "Departments ",
          "arabic": "الاقسام"
      },
      {
          "english": "Sector"
      },
      {
          "english": "Management "
      },
      {
          "english": "Department Ar Name",
          "arabic": "اسم القسم بالعربية"
      },
      {
          "english": "Department En Name",
          "arabic": "اسم القسم بالانكليزية"
      },
      {
          "english": "Department Ar Description",
          "arabic": "وصف القسم بالعربية"
      },
      {
          "english": "Department En Description",
          "arabic": "وصف القسم بالانكليزية "
      },
      {
          "english": "Auctions ",
          "arabic": "المزادات"
      },
      {
          "english": "Ar Title",
          "arabic": "العنوان بالعربىة"
      },
      {
          "english": "En Title",
          "arabic": "العنوان بالانكليزية"
      },
      {
          "english": "Ar Description ",
          "arabic": "الوصف بالعربية"
      },
      {
          "english": "En Description",
          "arabic": "الوصف بالانكليزية"
      },
      {
          "english": "Image",
          "arabic": "صورة"
      },
      {
          "english": "Date",
          "arabic": "التاريخ"
      },
      {
          "english": "Import Contacts",
          "arabic": "استيراد عملاء"
      },
      {
          "english": "Upload",
          "arabic": "رفع"
      },
      {
          "english": "Update Existing Data",
          "arabic": "تحديث البيانات الموجودة"
      },
      {
          "english": "Save",
          "arabic": "حفظ"
      },
      {
          "english": "Display Data",
          "arabic": "عرض البيانات"
      },
      {
          "english": "Download Template",
          "arabic": "تحميل نموذج"
      },
      {
          "english": "Mandatory Fields",
          "arabic": "الحقول الالزمية "
      },
      {
          "english": "First Name",
          "arabic": "الاسم الاول"
      },
      {
          "english": "Last Name",
          "arabic": "الاسم الاخير"
      },
      {
          "english": "Personal Mobile",
          "arabic": "الموبيل الشخصى"
      },
      {
          "english": "Personal Email",
          "arabic": "الايميل الشخصى"
      },
      {
          "english": "Import Deals",
          "arabic": "استيراد صفقات"
      },
      {
          "english": "Upload",
          "arabic": "رفع"
      },
      {
          "english": "Update Existing Data",
          "arabic": "تحديث البيانات الموجودة"
      },
      {
          "english": "Save",
          "arabic": "حفظ"
      },
      {
          "english": "Display Data",
          "arabic": "عرض البيانات"
      },
      {
          "english": "Download Template",
          "arabic": "تحميل نموذج"
      },
      {
          "english": "Mandatory Fields",
          "arabic": "الحقول الالزمية "
      },
      {
          "english": "Owner Name",
          "arabic": "اسم المالك"
      },
      {
          "english": "Client Name",
          "arabic": "اسم العميل"
      },
      {
          "english": "Deal Date",
          "arabic": "تاريخ الصفقة"
      },
      {
          "english": "Deal Status",
          "arabic": "حالة الصفقة"
      },
      {
          "english": "Import Leads",
          "arabic": "استيراد الصفقات  محتملة"
      },
      {
          "english": "Upload",
          "arabic": "رفع"
      },
      {
          "english": "Update Existing Data",
          "arabic": "تحديث البيانات الموجودة"
      },
      {
          "english": "Save",
          "arabic": "حفظ"
      },
      {
          "english": "Display Data",
          "arabic": "عرض البيانات"
      },
      {
          "english": "Download Template",
          "arabic": "تحميل نموذج"
      },
      {
          "english": "Mandatory Fields",
          "arabic": "الحقول الالزمية "
      },
      {
          "english": "First Name",
          "arabic": "الاسم الاول"
      },
      {
          "english": "Last Name",
          "arabic": "الاسم الاخير"
      },
      {
          "english": "Personal Mobile",
          "arabic": "الموبيل الشخصى"
      },
      {
          "english": "Personal Email",
          "arabic": "الايميل الشخصى"
      },
      {
          "english": "Import Listing",
          "arabic": "استيراد  عقارات "
      },
      {
          "english": "Upload",
          "arabic": "رفع"
      },
      {
          "english": "From Excel Shee",
          "arabic": "من ملف اكسيل"
      },
      {
          "english": "From XML File",
          "arabic": "من ملف اكس ام ال"
      },
      {
          "english": "Update Existing Data",
          "arabic": "تحديث البيانات الموجودة"
      },
      {
          "english": "Save",
          "arabic": "حفظ"
      },
      {
          "english": "Display Data",
          "arabic": "عرض البيانات"
      },
      {
          "english": "Download Template",
          "arabic": "تحميل نموذج"
      },
      {
          "english": "Mandatory Fields",
          "arabic": "الحقول الالزمية "
      },
      {
          "english": "Property Title",
          "arabic": "اسم العقار"
      },
      {
          "english": "Category",
          "arabic": "القسم"
      },
      {
          "english": "City",
          "arabic": "المدينة"
      },
      {
          "english": "Community",
          "arabic": "الوحدة السكنية"
      },
      {
          "english": "Description",
          "arabic": "الوصف"
      },
      {
          "english": "Price",
          "arabic": "السعر"
      },
      {
          "english": "BUA",
          "arabic": "مساحة العقار"
      },
      {
          "english": "Beds",
          "arabic": "الغرف"
      },
      {
          "english": "Email Settings",
          "arabic": "اعدادات البريد الالكترونى"
      },
      {
          "english": "User Name",
          "arabic": "اسم المستخدم"
      },
      {
          "english": "Email",
          "arabic": "الايميل"
      },
      {
          "english": "Passward",
          "arabic": "الباسورد"
      },
      {
          "english": "Confirm Passward",
          "arabic": "تاكيد الباسورد"
      },
      {
          "english": "Outgoing Mail Server(SMTP)",
          "arabic": "خادم البريد الصادر(SMTP)"
      },
      {
          "english": "Incoming Mail Server(POP3)",
          "arabic": "خادم البريد الوارد (POP3)"
      },
      {
          "english": "SMTP Port",
          "arabic": "منفذ (SMTP)"
      },
      {
          "english": "Reload Time",
          "arabic": "وقت التحميل"
      },
      {
          "english": "General Settings",
          "arabic": "اعدادات عامة"
      },
      {
          "english": "search by type",
          "arabic": "بحث بالنوع"
      },
      {
          "english": "Records Per Page",
          "arabic": " السجلات لكل صفحة"
      },
      {
          "english": "Type",
          "arabic": "النوع"
      },
      {
          "english": "Edit",
          "arabic": "تعديل"
      },
      {
          "english": "Unit Category",
          "arabic": "وحدة التصنيف"
      },
      {
          "english": "Unit Type",
          "arabic": "نوع الوحدة"
      },
      {
          "english": "unit sub community",
          "arabic": " الوحدة الفرعية للوحدة السكنية"
      },
      {
          "english": "Unit Purpose",
          "arabic": "وحدة الغرض"
      },
      {
          "english": "Unit Community",
          "arabic": "الوحدة للوحدات السكنية"
      },
      {
          "english": "Scope View",
          "arabic": "نطاق عرض"
      },
      {
          "english": "Contact Source",
          "arabic": "مصدر الاتصال"
      },
      {
          "english": "MR/MRS",
          "arabic": "السيد/السيدة"
      },
      {
          "english": "Payment Type",
          "arabic": "نوع الدفع"
      },
      {
          "english": "Property Status",
          "arabic": "حالة العقار"
      },
      {
          "english": "Projects",
          "arabic": "مشاريع"
      },
      {
          "english": "Annual Sub Type",
          "arabic": "النوع الفرعى السنوى"
      },
      {
          "english": "Room Number",
          "arabic": "رقم الغرفة"
      },
      {
          "english": "Annual Main Type",
          "arabic": " النوع الرئيسي السنوى"
      },
      {
          "english": "Bath Room Number",
          "arabic": "عدد الحمامات"
      },
      {
          "english": "Nationality",
          "arabic": "جنسية"
      },
      {
          "english": "Managers",
          "arabic": "مدراء"
      },
      {
          "english": "Lead Status",
          "arabic": "حالة المبيعات المحتملة"
      },
      {
          "english": "Lead Type",
          "arabic": "نوع المبيعات المحتملة"
      },
      {
          "english": "Location Area",
          "arabic": "مساحة الموقع"
      },
      {
          "english": "Gender",
          "arabic": "الجنس"
      },
      {
          "english": "Facilities Residential",
          "arabic": "مرافق سكنية"
      },
      {
          "english": "Facilities Commercial",
          "arabic": "التسهيلات التجارية"
      },
      {
          "english": "Image Property",
          "arabic": "صورة العقار"
      },
      {
          "english": "Contact Status",
          "arabic": "حالة الاتصال"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "About",
          "arabic": "حول"
      },
      {
          "english": "Building",
          "arabic": "بناء"
      },
      {
          "english": "Blog Category",
          "arabic": "بلوق الفئة"
      },
      {
          "english": "Users",
          "arabic": "المستخدمين"
      },
      {
          "english": "Name",
          "arabic": "اسم"
      },
      {
          "english": "Email",
          "arabic": "البريد الإلكتروني"
      },
      {
          "english": "Mobile",
          "arabic": "التليفون المحمول"
      },
      {
          "english": "Nationality",
          "arabic": "جنسية"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "DOB",
          "arabic": "تاريخ الميلاد"
      },
      {
          "english": "Active",
          "arabic": "نشط"
      },
      {
          "english": "Permissions",
          "arabic": "صلاحيات"
      },
      {
          "english": "Upload Photo",
          "arabic": "حمل الصورة"
      },
      {
          "english": "Male",
          "arabic": "ذكر"
      },
      {
          "english": "Female",
          "arabic": "أنثى"
      },
      {
          "english": "First Name",
          "arabic": "الاسم الاول"
      },
      {
          "english": "Last Name",
          "arabic": "الكنية"
      },
      {
          "english": "Nationality",
          "arabic": "جنسية"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Birth Date",
          "arabic": "تاريخ الميلاد"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Passward",
          "arabic": "كلمه السر"
      },
      {
          "english": "Confirm Passward",
          "arabic": "تأكيد كلمة المرور"
      },
      {
          "english": "Telephone",
          "arabic": "هاتف"
      },
      {
          "english": "Telephone 2",
          "arabic": "هاتف 2"
      },
      {
          "english": "Email",
          "arabic": "البريد الإلكتروني"
      },
      {
          "english": "Fax",
          "arabic": "فاكس"
      },
      {
          "english": "Po Box",
          "arabic": "ص.ب."
      },
      {
          "english": "Mobile1",
          "arabic": "التليفون المحمول1"
      },
      {
          "english": "Mobile2",
          "arabic": "التليفون المحمول2"
      },
      {
          "english": "User Type",
          "arabic": "نوع المستخدم"
      },
      {
          "english": "Notes",
          "arabic": "ملاحظات"
      },
      {
          "english": "Active",
          "arabic": "نشط"
      },
      {
          "english": "Male",
          "arabic": "ذكر"
      },
      {
          "english": "Female",
          "arabic": "أنثى"
      },
      {
          "english": "View User Permission ",
          "arabic": "مشاهدة صلاحيات المستخدم"
      },
      {
          "english": "Prohibited Access Forms",
          "arabic": "نوافذ حظر الوصول"
      },
      {
          "english": "Select All Forms",
          "arabic": "حدد كافة النوافذ"
      },
      {
          "english": "Web Blogs",
          "arabic": "المدونات على شبكة الإنترنت"
      },
      {
          "english": "Import Contact",
          "arabic": "استيراد الاتصال"
      },
      {
          "english": "Import Deals",
          "arabic": "استيراد صفقات"
      },
      {
          "english": "Import Leads",
          "arabic": "استيراد الصفقات المحتملة"
      },
      {
          "english": "Import Listing",
          "arabic": "استيراد عقارات"
      },
      {
          "english": "Email Setting",
          "arabic": "إعداد البريد الإلكتروني"
      },
      {
          "english": "Website Communities",
          "arabic": "موقع المجتمعات "
      },
      {
          "english": "Prices Index",
          "arabic": "مؤشر أسعار"
      },
      {
          "english": "About Us",
          "arabic": "معلومات عنا"
      },
      {
          "english": "Projects",
          "arabic": "مشاريع"
      },
      {
          "english": "Managers",
          "arabic": "مدراء"
      },
      {
          "english": "News",
          "arabic": "أخبار"
      },
      {
          "english": "Annual Reports",
          "arabic": "تقارير سنوية"
      },
      {
          "english": "Compounds",
          "arabic": "مجمعات سكنية"
      },
      {
          "english": "Sectors",
          "arabic": "القطاعات"
      },
      {
          "english": "Managments",
          "arabic": "إدارات"
      },
      {
          "english": "Departments",
          "arabic": "الاقسام"
      },
      {
          "english": "Buildings",
          "arabic": "البنايات"
      },
      {
          "english": "Auctions",
          "arabic": "مزادات"
      },
      {
          "english": "Add to Granted",
          "arabic": "إضافة إلى منح"
      },
      {
          "english": "Remove From Granted",
          "arabic": "إزالة من منح"
      },
      {
          "english": "Granted Access Forms",
          "arabic": "نوافذ منح الوصول"
      },
      {
          "english": "Form Name",
          "arabic": "اسم النافذة"
      },
      {
          "english": "Listing Sales",
          "arabic": " عقارات للبيع"
      },
      {
          "english": "Listing Sale",
          "arabic": " عقارات للبيع"
      },
      {
          "english": "Menu Setup",
          "arabic": "إعداد القائمة"
      },
      {
          "english": "عقارات للايجار",
          "arabic": "عقارات للايجار"
      },
      {
          "english": "Settings",
          "arabic": "إعدادات"
      },
      {
          "english": "Lead Sale Property",
          "arabic": "المبيعات المحتملة للعقار"
      },
      {
          "english": "Contacts",
          "arabic": "عملاء"
      },
      {
          "english": "Correspondence",
          "arabic": "متابعات"
      },
      {
          "english": "Correspondences",
          "arabic": "متابعات"
      },
      {
          "english": "Contracts",
          "arabic": "عقود"
      },
      {
          "english": "Contract",
          "arabic": "عقود"
      },
      {
          "english": "Users",
          "arabic": "المستخدمين"
      },
            {
                "english": "User",
                "arabic": "المستخدم"
            },
  {
      "english": "What Do You Like To View? ",
                            "arabic": "ماذا تحب ان ترى"
   },
      {
          "english": "Leads",
          "arabic": "صفقات محتملة"
      },
      {
          "english": " Dashboard",
          "arabic": "لوحة التحكم"
      },
      {
          "english": "Agents Statistics",
          "arabic": "احصائيات الوكلاء"
      },
      {
          "english": "Calls By Agent",
          "arabic": "المكالمات بواسطة وكيل"
      },
      {
          "english": "Viewing By Agent",
          "arabic": "الاطلاعواسطة وكيل"
      },
      {
          "english": "Meetings By Agent",
          "arabic": "اجتماعات بواسطة وكيل"
      },
      {
          "english": "Properties By Agent",
          "arabic": "العقارات عن طريق وكيل"
      },
      {
          "english": "Rent Property By Agent",
          "arabic": "استئجار العقارات بواسطة وكيل"
      },
      {
          "english": "Sales Property By Agent",
          "arabic": "مبيعات العقارات بواسطة وكيل"
      },
      {
          "english": "Contacts By Agent",
          "arabic": "اتصالات بواسطة وكيل"
      },
      {
          "english": "Landlords By Agent",
          "arabic": "الملاك بواسطة وكيل"
      },
      {
          "english": "Buyers/Tenants By Agent",
          "arabic": "المشترين / المستأجرين بواسطة وكيل"
      },
      {
          "english": "Report1",
          "arabic": "تقرير 1"
      },
      {
          "english": "Company Setup",
          "arabic": "إعداد شركة"
      },
      {
          "english": "Approve To Publish",
          "arabic": "الموافقة للنشر"
      },
      {
          "english": "Approve To UnPublish",
          "arabic": "الموافقة على عدم النشر"
      },
      {
          "english": "Calendar",
          "arabic": "تقويم"
      },
      {
          "english": "Mail",
          "arabic": "بريد"
      },
      {
          "english": "Map",
          "arabic": " خريطة"
      },
      {
          "english": "Lead List",
          "arabic": "قائمة المبيعات المحتملة"
      },
      {
          "english": "General Notice",
          "arabic": "اشعار عام"
      },
      {
          "english": "Agent Templates",
          "arabic": "نماذج وكيل"
      },
      {
          "english": "Company Templates",
          "arabic": "نماذج شركة"
      },
      {
          "english": "Address Templates",
          "arabic": "نماذج عنوان"
      },
      {
          "english": "Publish Settings",
          "arabic": " إعدادات النشر"
      },
      {
          "english": "Deals",
          "arabic": "صفقات"
      },
      {
          "english": "Company Setup",
          "arabic": "اعداد الشركة"
      },
      {
          "english": "Company Name",
          "arabic": "اسم الشركة"
      },
      {
          "english": "Company Email",
          "arabic": "البريد الالكترونى للشركة"
      },
      {
          "english": "Company Website",
          "arabic": "الموقع الالكترونى للشركة"
      },
      {
          "english": "Telephone",
          "arabic": "هاتف"
      },
      {
          "english": "Fax",
          "arabic": "فاكس"
      },
      {
          "english": "xcoordinate",
          "arabic": "احداثى س"
      },
      {
          "english": "ycoordinate",
          "arabic": "الإحداثي ص"
      },
      {
          "english": "Zip/postal Code",
          "arabic": "الرمز البريدي "
      },
      {
          "english": "Company Address",
          "arabic": "عنوان الشركة"
      },
      {
          "english": "About The Company",
          "arabic": "عن الشركة"
      },
      {
          "english": "Approve Publish",
          "arabic": "الموافقة على النشر"
      },
      {
          "english": "Approve Unpublish",
          "arabic": "الموافقة على إلغاء النشر"
      },
      {
          "english": "Upload Logo Photo",
          "arabic": "تحميل صورة الشعار "
      },
      {
          "english": "Upload Documents",
          "arabic": "تحميل وثائق"
      },
      {
          "english": "Photo",
          "arabic": "صورة "
      },
      {
          "english": "Name",
          "arabic": "اسم"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "Website",
          "arabic": "موقع الكتروني"
      },
      {
          "english": "Web Blog",
          "arabic": " مدونة الكتومية"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Blog Date",
          "arabic": "تاريخ التدوينة"
      },
      {
          "english": "Description",
          "arabic": "وصف"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "Agent",
          "arabic": "وكيل"
      },
      {
          "english": "Back End",
          "arabic": "الواجهة الخلفية"
      },
      {
          "english": "Basic",
          "arabic": "الأساسية"
      },
      {
          "english": "Element",
          "arabic": "العنصر"
      },
      {
          "english": "Home",
          "arabic": "الصفحة الرئيسية"
      },
      {
          "english": "Sale ",
          "arabic": "تخفيضات"
      },
      {
          "english": "Sold",
          "arabic": "تم البيع"
      },
      {
          "english": "Images",
          "arabic": "صور"
      },
      {
          "english": "Update",
          "arabic": "تحديث"
      },
      {
          "english": "Delete",
          "arabic": "حذف"
      },
      {
          "english": "Copy",
          "arabic": "نسخ"
      },
      {
          "english": "CSV",
          "arabic": "اكسيل"
      },
      {
          "english": "Print",
          "arabic": "طباعة"
      },
      {
          "english": "Show",
          "arabic": "عرض"
      },
      {
          "english": "Entries",
          "arabic": "سجلات"
      },
      {
          "english": "Search",
          "arabic": "بحث"
      },
      {
          "english": "Show/Hide Columns",
          "arabic": "إظهار / إخفاء الأعمدة"
      },
      {
          "english": "Created At",
          "arabic": "أنشئت في"
      },
      {
          "english": "Created By",
          "arabic": "تم إنشاؤها بواسطة"
      },
      {
          "english": "Changed Date",
          "arabic": "تاريخ التغيير"
      },
      {
          "english": "Changed By",
          "arabic": "تم التغيير من قبل"
      },
      {
          "english": "Website Communities",
          "arabic": "المجتمعات الموقع"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Description",
          "arabic": "وصف"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Images",
          "arabic": "صور"
      },
      {
          "english": "No data available in table",
          "arabic": "لا توجد بيانات في الجدول"
      },
      {
          "english": "Showing 0 to 0 of 0 entries",
          "arabic": "عرض 0-0 من 0 سجلات"
      },
      {
          "english": "Previous",
          "arabic": "سابق"
      },
      {
          "english": "Next",
          "arabic": "التالى"
      },
      {
          "english": "Price Index",
          "arabic": "مؤشر الأسعار"
      },
      {
          "english": "Purpose",
          "arabic": "غرض"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Sub Community",
          "arabic": "مجتمع اصغر"
      },
      {
          "english": "Select Bedrooms",
          "arabic": "اختر غرف النوم"
      },
      {
          "english": "Min Price",
          "arabic": "اقل سعر"
      },
      {
          "english": "Max Price",
          "arabic": "أعلى سعر"
      },
      {
          "english": "AgentStatistics",
          "arabic": "احصائيات الوكلاء"
      },
      {
          "english": "Agent Name",
          "arabic": "اسم الوكيل"
      },
      {
          "english": "No Of Calls",
          "arabic": "عدد المكالمات"
      },
      {
          "english": "No Of Emails",
          "arabic": "عدد رسائل البريد الإلكتروني"
      },
      {
          "english": "No Of Viewing",
          "arabic": "عدد المشاهدات"
      },
      {
          "english": "No Of Meeting",
          "arabic": "عدد اجتماعات"
      },
      {
          "english": "No Of Clients",
          "arabic": "عدد العملاء"
      },
      {
          "english": "No Of Owners",
          "arabic": "عدد الملاك"
      },
      {
          "english": "No Of Properties",
          "arabic": "عدد العقارات"
      },
      {
          "english": "Calls By Agent",
          "arabic": "المكالمات بواسطة وكيل"
      },
      {
          "english": "Subject",
          "arabic": "موضوع"
      },
      {
          "english": "Contact",
          "arabic": "اتصال"
      },
      {
          "english": "Call Date",
          "arabic": "تاريخ الاتصال"
      },
      {
          "english": "Call Time",
          "arabic": "وقت الاتصال"
      },
      {
          "english": "Agent",
          "arabic": "وكيل"
      },
      {
          "english": "Viewing By Agent",
          "arabic": "الاطلاع على  بواسطة وكيل"
      },
      {
          "english": "Subject",
          "arabic": "موضوع"
      },
      {
          "english": "Contact",
          "arabic": "اتصال"
      },
      {
          "english": "Call Date",
          "arabic": "تاريخ الاتصال"
      },
      {
          "english": "Call Time",
          "arabic": "وقت الاتصال"
      },
      {
          "english": "Agent",
          "arabic": "وكيل"
      },
      {
          "english": "Meeting By Agent",
          "arabic": "الاجتماع وكيل"
      },
      {
          "english": "Subject",
          "arabic": "موضوع"
      },
      {
          "english": "Contact",
          "arabic": "اتصال"
      },
      {
          "english": "Call Date",
          "arabic": "تاريخ الاتصال"
      },
      {
          "english": "Call Time",
          "arabic": "وقت الاتصال"
      },
      {
          "english": "Agent",
          "arabic": "وكيل"
      },
      {
          "english": "Properties By Agent",
          "arabic": "العقارات عن طريق وكيل"
      },
      {
          "english": "Rent Property By Agent",
          "arabic": "استئجار العقارات بواسطة وكيل"
      },
      {
          "english": "Sales Property By Egent",
          "arabic": "مبيعات العقارات بواسطة وكيل"
      },
      {
          "english": "Code",
          "arabic": "رمز"
      },
      {
          "english": "Rent Or Sale",
          "arabic": "إيجار أو بيع"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Sub Community",
          "arabic": "مجتمع فرعى"
      },
      {
          "english": "Agent",
          "arabic": "وكيل"
      },
      {
          "english": "Contacts By Agent",
          "arabic": "اتصالات بواسطة وكيل"
      },
      {
          "english": "Code",
          "arabic": "رمز"
      },
      {
          "english": "Contact Name",
          "arabic": "اسم جهة الاتصال"
      },
      {
          "english": "Contact Type",
          "arabic": "نوع الاتصال"
      },
      {
          "english": "Contact Source",
          "arabic": "مصدر الاتصال"
      },
      {
          "english": "Personal Mobile",
          "arabic": "موبايل الشخصي"
      },
      {
          "english": "Personal Email",
          "arabic": "البريد الإلكتروني الشخصي"
      },
      {
          "english": "Landlords By Agent",
          "arabic": "الملاك بواسطة وكيل"
      },
      {
          "english": "Code",
          "arabic": "رمز"
      },
      {
          "english": "Contact Name",
          "arabic": "اسم جهة الاتصال"
      },
      {
          "english": "Contact Type",
          "arabic": "نوع الاتصال"
      },
      {
          "english": "Contact Source",
          "arabic": "مصدر الاتصال"
      },
      {
          "english": "Personal Mobile",
          "arabic": "موبايل الشخصي"
      },
      {
          "english": "Personal Email",
          "arabic": "البريد الإلكتروني الشخصي"
      },
      {
          "english": "Buyers/tenants By Agent",
          "arabic": "المشترين / المستأجرين بواسطة وكيل"
      },
      {
          "english": "Code",
          "arabic": "رمز"
      },
      {
          "english": "Contact Name",
          "arabic": "اسم جهة الاتصال"
      },
      {
          "english": "Contact Type",
          "arabic": "نوع الاتصال"
      },
      {
          "english": "Contact Source",
          "arabic": "مصدر الاتصال"
      },
      {
          "english": "Personal Mobile",
          "arabic": "موبايل الشخصي"
      },
      {
          "english": "Personal Email",
          "arabic": "البريد الإلكتروني الشخصي"
      },
      {
          "english": "Dashboard",
          "arabic": "الرئيسية"
      },
      {
          "english": "My Contracts",
          "arabic": "العقود"
      },
      {
          "english": "My Leads",
          "arabic": "صفقات محتملة"
      },
      {
          "english": "My Contacts",
          "arabic": "العملاء"
      },
      {
          "english": "Clock",
          "arabic": "التوقيت"
      },
      {
          "english": "Calendar",
          "arabic": "ألتقويم"
      },
      {
          "english": "Followup List",
          "arabic": "المتابعات"
      },
      {
          "english": "List",
          "arabic": "القائمة"
      },
      {
          "english": "Archive",
          "arabic": "الارشيف"
      },
      {
          "english": "My NOTICE",
          "arabic": "ملاحظات خاصة"
      },
      {
          "english": "GENERAL NOTICE",
          "arabic": "ملاحظات عامة"
      },
      {
          "english": "Send SMS",
          "arabic": "ارسال رسالة تليفونية"
      },
      {
          "english": "Message ",
          "arabic": "نص الرسالة"
      },
      {
          "english": "Mobile Number",
          "arabic": "رقم التليفون"
      },
      {
          "english": "More",
          "arabic": "المزيد"
      },
      {
          "english": "Property Code",
          "arabic": "كود العقار"
      },
      {
          "english": "Property Title",
          "arabic": "اسم العقار"
      },
      {
          "english": "Short Title",
          "arabic": "اسم مختصر"
      },
      {
          "english": "Type",
          "arabic": "نوع العقار"
      },
      {
          "english": "Bedrooms",
          "arabic": "غرف المعيشة"
      },
      {
          "english": "Price",
          "arabic": "السعر"
      },
      {
          "english": "Unit No",
          "arabic": "رقم العقار"
      },
      {
          "english": "Category",
          "arabic": "تصنيف العقار"
      },
      {
          "english": "Bathrooms",
          "arabic": "حمامات"
      },
      {
          "english": "Price / sq ft",
          "arabic": "سعر القدم المربع "
      },
      {
          "english": "Listed Date",
          "arabic": "تاريخ الادخال"
      },
      {
          "english": "City",
          "arabic": "المدينة"
      },
      {
          "english": "Compound",
          "arabic": "المجمع السكنى"
      },
      {
          "english": "Street No",
          "arabic": "رقم الشارع"
      },
      {
          "english": "Property Description",
          "arabic": "وصف العقار"
      },
      {
          "english": "Last Update",
          "arabic": "اخر تحديث"
      },
      {
          "english": "Community",
          "arabic": "الوحدة تابع لها العقار"
      },
      {
          "english": "Building",
          "arabic": "المبنى الموجود به العقار"
      },
      {
          "english": "Facilities",
          "arabic": "المرافق"
      },
      {
          "english": "Plot No",
          "arabic": "رقم قطعة الارض"
      },
      {
          "english": "Floor Number",
          "arabic": "الطابق"
      },
      {
          "english": "Photos",
          "arabic": "صور"
      },
      {
          "english": "Owner Name",
          "arabic": "المالك"
      },
      {
          "english": "Portals",
          "arabic": "مواقع النشر"
      },
      {
          "english": "Filtered Enquiry",
          "arabic": "الاستفسارات المتشابهة"
      },
      {
          "english": "Plot Size",
          "arabic": "مساحة الارض"
      },
      {
          "english": "Built Up Area (sqft)",
          "arabic": "المساحة بالقدم "
      },
      {
          "english": "Agent",
          "arabic": "الوكيل"
      },
      {
          "english": "Scope Of View",
          "arabic": "المنظر الخارجى"
      },
      {
          "english": "Commission Rate",
          "arabic": "نسبة العمولة"
      },
      {
          "english": "Status",
          "arabic": "حالة العقار"
      },
      {
          "english": "DEWA Number"
      },
      {
          "english": "Developer",
          "arabic": "شركة البناء"
      },
      {
          "english": "Publish Status",
          "arabic": "حالة النشر"
      },
      {
                "english": "Publish",
                "arabic": "نشر"
      },
     {
                "english": "unPublish",
                "arabic": "الغاء النشر"
     },
     {
              "english": "matched leads",
              "arabic": "الاستفسارات المتشابهة"
     },
     {
                        "english": "collection update",
                        "arabic": "تحديث تاريخ الادخال"
     },
     {
         "english": "Share Option",
         "arabic": "خيارات المشاركة"
     },
     {
         "english": "Show Map",
         "arabic": "الخريطة"
     },
     {
         "english": "Assign",
         "arabic": "تحديد وكيل"
     },
     {
         "english": "un archive",
         "arabic": "الغاء الارشفة"
     },
     {
             "english": "Advanced Options",
             "arabic": "خيارات متقدمة"
     },
     {
         "english": "search",
         "arabic": "بحث"
     },
     {
         "english": "Current Listing",
              "arabic": "العقارات الحالية"
     },
     {
         "english": "Archived Listing",
         "arabic": "الارشيف"
},
{
         "english": "Show / hide columns",
         "arabic": "اظهار واخفاء الاعمدة"
},
{
    "english": "Show",
    "arabic": "عدد الصفوف"
},
{
    "english": "entries",
    "arabic": "صف"
},
      {
          "english": "STR"
      },
      {
          "english": "LandMark",
          "arabic": "المعالم الرئيسية"
      },
      {
          "english": "Commission Amount",
          "arabic": "قيمة العمولة"
      },
      {
          "english": "Source",
          "arabic": "المصدر"
      },
      {
          "english": "Next available",
          "arabic": "متاح فى "
      },
      {
          "english": "Publish Status",
          "arabic": "حالة النشر"
      },
      {
          "english": "Deposit Rate",
          "arabic": "نسبة التأمين"
      },
      {
          "english": "Furnish Status",
          "arabic": "الديكور"
      },
      {
          "english": "Remind",
          "arabic": "تذكار"
      },
      {
          "english": "Rented Until",
          "arabic": "تم التأجير حتى"
      },
      {
          "english": "Other Media",
          "arabic": "فيديوهات اخرى"
      },
      {
          "english": "Maintenance Fee",
          "arabic": "رسوم الصيانة "
      },
      {
          "english": "x-coordinate"
      },
      {
          "english": "y-coordinate"
      },
      {
          "english": "Notes",
          "arabic": "ملاحظات"
      },
      {
          "english": "Featured",
          "arabic": "مميزة"
      },
      {
          "english": "Property Rented?",
          "arabic": "تم الايجار"
      },
      {
          "english": "Refered By Agent",
          "arabic": "تم المراجعة"
      },
      {
          "english": "Managed",
           "arabic": "تم الادراة"
      },
      {
          "english": "Exclusive",
          "arabic": "حصرى"
      },
      {
          "english": "Unit Developed",
          "arabic": "تم تشييدها "
      },
      {
          "english": "Cheques",
          "arabic": "الشيكات"
      },
      {
          "english": "Compound Code",
          "arabic": "كود المجمع"
      },
      {
          "english": "Compound Name1",
          "arabic": "اسم المجمع 1"
      },
      {
          "english": "Compound Name2",
          "arabic": "اسم المجمع 2"
      },
      {
          "english": "Compound Address1",
          "arabic": "عنوان المجمع 1"
      },
      {
          "english": "Compound Address2",
          "arabic": "عنوان المجمع 2"
      },
      {
          "english": "Compound Description",
          "arabic": "الوصف"
      },
      {
          "english": "Create Date",
          "arabic": "تاريخ التسجيل"
      },
      {
          "english": "Building Code",
          "arabic": "كود المبنى"
      },
      {
          "english": "Building Name1",
          "arabic": "اسم المبنى 1"
      },
      {
          "english": "Building Name2",
          "arabic": "اسم المبنى 2"
      },
      {
          "english": "Building Description",
          "arabic": "الوصف"
      },
      {
          "english": "Leads",
          "arabic": "صفقات محتملة"
      },
      {
          "english": "Code",
          "arabic": "الكود"
      },
      {
          "english": "Contact Type",
          "arabic": "نوع العميل"
      },
      {
          "english": "Status",
          "arabic": "حالة الصفقة"
      },
      {
          "english": "Lead Date",
          "arabic": "تاريخ الصفقة"
      },
      {
          "english": "Contact",
          "arabic": "العميل"
      },
      {
          "english": "Sub Status",
          "arabic": "الحالة الفرعية"
      },
      {
          "english": "Priority",
          "arabic": "الاهمية"
      },
      {
          "english": "Source",
          "arabic": "المصدر"
      },
      {
          "english": "Agent",
          "arabic": "الوكيل"
      },
      {
          "english": "Type",
          "arabic": "النوع"
      },
      {
          "english": "Properties",
          "arabic": "العقارات المرتبطة بالصفقة"
      },
      {
          "english": "Share",
          "arabic": "مشاركة"
      },
      {
          "english": "Documents",
          "arabic": "مستندات"
      },
      {
          "english": "Basic Details",
          "arabic": "البيانات الاساسية"
      },
      {
          "english": "Ref",
          "arabic": "الكود"
      },
      {
          "english": "Birthdate",
          "arabic": "تاريخ الميلاد"
      },
      {
          "english": "Name",
          "arabic": "الاسم"
      },
      {
          "english": "Nationality",
          "arabic": "الجنسية"
      },
      {
          "english": "Language",
          "arabic": "اللغة"
      },
      {
          "english": "Company",
          "arabic": "اسم الشركة"
      },
      {
          "english": "Designation",
          "arabic": "لقب الشركة"
      },
      {
          "english": "Website",
          "arabic": "الموقع الالكترونى"
      },
      {
          "english": "Other ",
          "arabic": "بيانات اخرى"
      },
      {
          "english": "Assigned To",
          "arabic": "الوكيل"
      },
      {
          "english": "Contact Source",
          "arabic": "المصدر"
      },
      {
          "english": "Contact Status",
          "arabic": "حالة العميل"
      },
      {
          "english": "Category",
          "arabic": "الفئة"
      },
      {
          "english": "Sub Category",
          "arabic": "الفئة الفرعية"
      },
      {
          "english": "Personal Contact",
          "arabic": "وسائل الاتصال الشخصية"
      },
      {
          "english": "Mobile",
          "arabic": "الجوال"
      },
      {
          "english": "Phone",
          "arabic": "التليفون"
      },
      {
          "english": "Fax",
          "arabic": "الفاكس"
      },
      {
          "english": "Email",
          "arabic": "البريد الالكترونى"
      },
      {
          "english": "Address",
          "arabic": "العنوان"
      },
      {
          "english": "Work Contact",
          "arabic": "وسائل الاتصال بالعمل"
      },
      {
          "english": "Other Contact",
          "arabic": "وسائل اتصال اخرى"
      },
      {
          "english": "Social Media",
          "arabic": "وسائل التواصل الاجتماعى"
      },
      {
          "english": "Documents",
          "arabic": "الملفات"
      },
      {
          "english": "First Name",
          "arabic": "الاسم الاول"
      },
      {
          "english": "Last Name",
          "arabic": "الاسم الاخير"
      },
      {
          "english": "Deals",
          "arabic": "الصفقات"
      },
      {
          "english": "Buyer",
          "arabic": "المشترى"
      },
      {
          "english": "Seller",
          "arabic": "البائع"
      },
      {
          "english": "Deal Date",
          "arabic": "تاريخ الصفقة"
      },
      {
          "english": "Deal Type",
          "arabic": "نوع الصفقة"
      },
      {
          "english": "Price",
          "arabic": "السعر"
      },
      {
          "english": "Deposit",
          "arabic": "التأمين"
      },
      {
          "english": "Total Commission",
          "arabic": "العمولة"
      },
      {
          "english": "Deposit Per",
          "arabic": "نسبة التأمين"
      },
      {
          "english": "Total Commission Per",
          "arabic": "نسبة كامل العمولة"
      },
      {
          "english": "Agent's Commission",
          "arabic": "الوكيل"
      },
      {
          "english": "Property Code",
          "arabic": "كود العقار"
      },
      {
          "english": "Details",
          "arabic": "التفاصيل"
      },
      {
          "english": "Documents",
          "arabic": "الملفات"
      },
      {
          "english": "Correspondance",
          "arabic": "متابعات"
      },
      {
          "english": "Contact",
          "arabic": "العميل"
      },
      {
          "english": "Priority",
          "arabic": "الاهمية"
      },
      {
          "english": "Correspondence Date",
          "arabic": "تاريخ المتابعة"
      },
      {
          "english": "Correspondence Type",
          "arabic": "نوع المتابعة"
      },
      {
          "english": "Correspondence Time",
          "arabic": "وقت المتابعة"
      },
      {
          "english": "in",
          "arabic": "فى الداخل"
      },
      {
          "english": "out",
          "arabic": "فى الخارج"
      },
      {
          "english": "Location",
          "arabic": "الموقع"
      },
      {
          "english": "Remarks",
          "arabic": "ملاحظات"
      },
      {
          "english": "Description",
          "arabic": "الوصف"
      },
      {
          "english": "Listing Code",
          "arabic": "كود العقار"
      },
      {
          "english": "Follow Up",
          "arabic": "متابعة"
      },
      {
          "english": "Category",
          "arabic": "الفئة"
      },
      {
          "english": "Beds",
          "arabic": "غرف المعيشة"
      },
      {
          "english": "Community",
          "arabic": "الوحدة التابع لها العقار"
      },
      {
          "english": "Lead Code",
          "arabic": "رقم الصفقة المحتملة"
      },
      {
          "english": "Agent",
          "arabic": "الوكيل"
      },
      {
          "english": "Sub Community",
          "arabic": "الوحدة الفرعية التابع لها العقار"
      },
      {
          "english": "Contact",
          "arabic": "عقد"
      },
      {
          "english": "Owner Name",
          "arabic": "مالك العقار"
      },
      {
          "english": "Buyer/Tenant 2",
          "arabic": "المشترى / المؤجر 2"
      },
      {
          "english": "Buyer/Tenant 1",
          "arabic": "المشترى / المؤجر1"
      },
      {
          "english": "Deal Code",
          "arabic": "كود الصفقة"
      },
      {
          "english": "Contract Date",
          "arabic": "تاريخ العقد"
      },
      {
          "english": "Expiry Date",
          "arabic": "تاريخ انتهاء العقد"
      },
      {
          "english": "cancel",
          "arabic": "الغاء"
      },
      {
          "english": "Amount",
          "arabic": "الكمية"
      },
      {
          "english": "Mail",
          "arabic": "بريد"
      },
      {
          "english": "Map",
          "arabic": " خريطة"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Notes",
          "arabic": "ملاحظات"
      },
      {
          "english": "Is Public",
          "arabic": "عام"
      },
      {
          "english": "Agent Template",
          "arabic": "نموذج وكيل"
      },
      {
          "english": "Agent ",
          "arabic": "وكيل"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Notes",
          "arabic": "ملاجظات"
      },
      {
          "english": "Company Template",
          "arabic": "نموذج شركة"
      },
      {
          "english": "Company Name",
          "arabic": "اسم الشركة"
      },
      {
          "english": "Company Details",
          "arabic": "بيانات الشركة"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Notes",
          "arabic": "ملاحظات"
      },
      {
          "english": "Location Template",
          "arabic": "نموذج موقع"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Title",
          "arabic": "عنوان"
      },
      {
          "english": "Notes",
          "arabic": "ملاحظات"
      },
      {
          "english": "Approve to publish",
          "arabic": "الموافقة على النشر"
      },
      {
          "english": "Reject Publish",
          "arabic": "نرفض النشر"
      },
      {
          "english": "Property Name",
          "arabic": "اسم الملكية"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "Price",
          "arabic": "السعر"
      },
      {
          "english": "Total Area",
          "arabic": "المساحة الكلية"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Sub Community",
          "arabic": "مجتمع فرعى"
      },
      {
          "english": "Agent Name",
          "arabic": "اسم الوكيل"
      },
      {
          "english": "Purpose",
          "arabic": "غرض"
      },
      {
          "english": "Approve to Unpublish",
          "arabic": "الموافقة على نشر"
      },
      {
          "english": "Property Title",
          "arabic": "عنوان الملكية"
      },
      {
          "english": "Purpose",
          "arabic": "غرض"
      },
      {
          "english": "Category",
          "arabic": "فئة"
      },
      {
          "english": "Price",
          "arabic": "السعر"
      },
      {
          "english": "Total Area",
          "arabic": "المساحة الكلية"
      },
      {
          "english": "City",
          "arabic": "مدينة"
      },
      {
          "english": "Community",
          "arabic": "مجتمع"
      },
      {
          "english": "Sub Community",
          "arabic": "مجتمع فرعى"
      },
      {
          "english": "Agent Name",
          "arabic": "اسم الوكيل"
      },
      {
          "english": "Purpose",
          "arabic": "غرض"
      },
      {
          "english": "Website Name",
          "arabic": "اسم الموقع"
      },
      {
          "english": "XML File",
          "arabic": "ملف XML"
      },
      {
          "english": "XML File",
          "arabic": "ملف XML"
      },
      {
          "english": "Clear All",
          "arabic": "مسح الكل"
      },
            {
                "english": "New",
                "arabic": "جديد"
            },
      {
          "english": "Delete",
          "arabic": "حذف"
      },
      {
          "english": "Edit",
          "arabic": "تعديل"
      },
      {
          "english": "building name",
          "arabic": "البناية"
      },
      {
          "english": "Listed",
          "arabic": "تاريخ الادخال"
      },
      {
          "english": "updated",
          "arabic": "اخر تحديث"
      },
      {
          "english": "Owner",
          "arabic": "مالك العقار"
      },
      {
          "english": "Baths",
          "arabic": "غرف المعيشة"
      },
      {
          "english": "Baths",
          "arabic": "غرف المعيشة"
      },
      {
          "english": "Floor NO",
          "arabic": "الطابق"
      },
      {
          "english": "commission",
          "arabic": "عمولة"
      },
      {
          "english": "Print Brochure",
          "arabic": "طباعة بروشور"
      },
      {
          "english": "Add Lead",
          "arabic": "اضافة صفقة محتملة"
      },
      {
          "english": "Add Contract",
          "arabic": "اضافة عقد"
      },
      {
          "english": "Add Deal",
          "arabic": "اضافة صفقة"
      },
      {
          "english": "Add Correspondance",
          "arabic": "اضافة متابعة"
      },
      {
          "english": "Action",
          "arabic": "خيارات سريعة"
      },
      {
          "english": "Filterd Enquiry",
          "arabic": "استفسارات متششابهة"
      },
      {
          "english": "Matches Properties",
          "arabic": "العقارات المتشابهة"
      },
      {
          "english": "Code",
          "arabic": "الكود"
      },
      {
          "english": "Is Shared",
          "arabic": "تم المشاركة"
      },
      {
          "english": "Last Updated",
          "arabic": "اخر تعديل"
      },
      {
          "english": "Select",
          "arabic": "اختر"
      },
      {
          "english": "Skype",
          "arabic": "سكيب"
      },
      {
          "english": "facebook",
          "arabic": "فيس بوك"
      },
      {
          "english": "twitter",
          "arabic": "تويتر"
      },
      {
          "english": "g+",
          "arabic": "جوجل بلس"
      },
      {
          "english": "Home Country",
          "arabic": "الدولة"
      },
      {
          "english": "Home Phone",
          "arabic": "التليفون "
      },
      {
          "english": "Assign to",
          "arabic": "الوكيل "
      },
      {
          "english": "nationality2",
          "arabic": "الجنسية الثانية "
      },
      {
          "english": "nationality1",
          "arabic": "الجنسية الاولى "
      },
      {
          "english": "work address1",
          "arabic": "عنوان العمل "
      },
      {
          "english": "work address2",
          "arabic": "عنوان العمل الثانى "
      },
      {
          "english": "work city",
          "arabic": " مدينة العمل"
      },
      {
          "english": "work country",
          "arabic": " دولة العمل"
      },
      {
          "english": "zip code",
          "arabic": " الرقم البريدى"
      },
      {
          "english": "native language",
          "arabic": "اللغة الرئيسية"
      },
      {
          "english": "second language",
          "arabic": "اللغة الثانية"
      },
      {
          "english": "created date",
          "arabic": "تاريخ الادخال"
      },
      {
          "english": "Add Listing",
          "arabic": "اضافة عقار"
      },
      {
          "english": "property details",
          "arabic": "بيانات العقار"
      },
      {
          "english": "Commission Details",
          "arabic": "بيانات السعر و العمولة"
      },
      {
          "english": "Buyer",
          "arabic": "المشترى"
      },
      {
          "english": "Tenant",
          "arabic": "المستأجر"
      },
      {
          "english": "Buyer / Tenant",
          "arabic": "المشترى /المستأجر"
      },
      {
          "english": "Seller",
          "arabic": "البائع"
      },
      {
          "english": "Landlord",
          "arabic": "المالك"
      },
      {
          "english": "Seller / Landlord",
          "arabic": "البائع /المالك"
      },
      {
          "english": "Floor",
          "arabic": "الطابق"
      },
      {
          "english": "Renwal Date",
          "arabic": "تاريخ التجديد"
      },
      {
          "english": "Start Date",
          "arabic": "تاريخ البداية"
      },
      {
          "english": "Start Date",
          "arabic": "تاريخ النهاية"
      },
      {
          "english": "Ref",
          "arabic": "الكود"
      },
      {
          "english": "Search Contact",
          "arabic": "ابحث عن عميل"
      },
      {
          "english": "Add New Contact",
          "arabic": "عميل جديد"
      },
      {
          "english": "New Contact",
          "arabic": "عميل جديد"
      },
      {
          "english": "SaveClose",
          "arabic": "حفظ  و إغلاق"
      },
      {
          "english": "Submit",
          "arabic": "حفظ"
      },
      {
          "english": "Commission Per",
          "arabic": "نسبة العمولة"
      },
      {
          "english": "Call",
          "arabic": "مكالمة"
      },
      {
          "english": "Courier",
          "arabic": "البريد السريع"
      },
      {
          "english": "Meeting",
          "arabic": "أجتماع"
      },
      {
          "english": "viewing",
          "arabic": "رؤية"
      },
      {
          "english": "viewing",
          "arabic": "رؤية"
      },
      {
          "english": "Courier Reference",
          "arabic": "كود البريد "
      },
      {
          "english": "Reciever Courier Reference",
          "arabic": "المستلم "
      },
      {
          "english": "Delivered",
          "arabic": "تم الاستلام "
      },
      {
          "english": "completed",
          "arabic": "انتهت "
      },
      {
          "english": "in progress",
          "arabic": "فى حيذ التنفيذ "
      },
      {
          "english": "not yet started",
          "arabic": "لم تبدأ"
      },
      {
          "english": "high",
          "arabic": "عالية الاهمية"
      },
      {
          "english": "low",
          "arabic": "قليلة الاهمية"
      },
      {
          "english": "medium",
          "arabic": "متوسطة الاهمية"
      },
      {
          "english": "normal",
          "arabic": "عادية"
      },
      {
          "english": "urgent",
          "arabic": "مستعجلة"
      },
      {
          "english": "refprop",
          "arabic": "رقم العقار"
      },
      {
          "english": "delivery date",
          "arabic": "تاريخ الاستلام"
      },
      {
          "english": "follow up  date",
          "arabic": "تاريخ المتابعة"
      },
      {
          "english": "in/out",
          "arabic": "داخل/خارج"
      },
      {
          "english": "CorRef",
          "arabic": "كود المتابعة"
      },
      {
          "english": "RCorRef",
          "arabic": "كود الرد على المتابعة"
      },
      {
          "english": "Remark",
          "arabic": "ملاحظات"
      },
      {
          "english": "Contract Code",
          "arabic": "الكود"
      },
      {
          "english": "Rent",
          "arabic": "إيجار"
      },
      {
          "english": "Approve Publish",
          "arabic": "موافقة على النشر"
      },
      {
          "english": "Reject Publish",
          "arabic": "رفض النشر"
      },
      {
          "english": "Click here to Configure",
          "arabic": "تهيئة"
      },
      {
          "english": "Reject UnPublish",
          "arabic": "رفض عدم النشر"
      },
      {
          "english": "modified date",
          "arabic": "تاريخ التعديل"
      },
      {
          "english": "Logout",
          "arabic": "خروج"
      },
      {
          "english": "commercial",
          "arabic": "تجارى"
      },
      {
          "english": "residential",
          "arabic": "سكنى"
      },
      {
          "english": "furnished",
          "arabic": "مفروشة"
      },
      {
          "english": "partly furnished",
          "arabic": "  جزئيا مفروشة"
      },
      {
          "english": "Unfurnished",
          "arabic": "غير مفروشة "
      },
      {
          "english": "Ref. Property Code",
          "arabic": " كود العقار"
      },
      {
          "english": "Max Bedrooms",
          "arabic": " اقصى عدد غرف"
      },
      {
          "english": "Min Bedrooms",
          "arabic": " اقل عدد غرف"
      },
      {
          "english": "Min Area",
          "arabic": " اقل مساحة"
      },
      {
          "english": "Max Area",
          "arabic": " اكبر مساحة"
      },
      {
          "english": "Ref.",
          "arabic": "كود"
      },
      {
          "english": "Hold.",
          "arabic": "مستمرة"
      },
      {
          "english": "inactive.",
          "arabic": "غير نشطة"
      },
      {
          "english": "Country.",
          "arabic": "الدولة"
      },
      {
          "english": "Address Line 1.",
          "arabic": "العنوان"
      },
      {
          "english": "Address Line 2.",
          "arabic": "العنوان2 "
      },
      {
          "english": "Sub Location",
          "arabic": " الموقع الفرعى"
      },
      {
          "english": "Rent Start Date",
          "arabic": " بداية الايجار"
      },
      {
          "english": "Renewal Date",
          "arabic": "تاريخ التجديد"
      },
      {
          "english": "Rent End Date",
          "arabic": "انتهاء الايجار"
      },
      {
          "english": "Rent End Date",
          "arabic": "انتهاء الايجار"
      },
      {
          "english": "closed",
          "arabic": "مغلق"
      },
      {
          "english": "opended",
          "arabic": "مفتوح"
      },
      {
          "english": "Unsuccessful",
          "arabic": "غير ناجح"
      },
      {
          "english": "NIL",
          "arabic": "لا شئ"
      },
      {
          "english": "successful",
          "arabic": "ناجح"
      },
      {
          "english": "Pending Completion",
          "arabic": "فى انتظار الاستكمال"
      },
      {
          "english": "Pending Signature",
          "arabic": "فى انتظار التوقيع"
      },
      {
          "english": "New Correspondence",
          "arabic": "متابعة جديدة"
      },
      {
          "english": "My Calendars ",
          "arabic": "التقويم"
      },
      {
          "english": "My Calendars ",
          "arabic": "التقويم"
      },
      {
          "english": "Today",
          "arabic": "اليوم"
      },
      {
          "english": "Today",
          "arabic": "اليوم"
      },
      {
          "english": "sun",
          "arabic": "الاحد"
      },
      {
          "english": "sat",
          "arabic": "السبت"
      },
      {
          "english": "mon",
          "arabic": "الاثنين"
      },
      {
          "english": "tue",
          "arabic": "الثلاثاء"
      },
      {
          "english": "wed",
          "arabic": "الاربعاء"
      },
      {
          "english": "thu",
          "arabic": "الخميس"
      },
      {
          "english": "fri",
          "arabic": "الجمعة"
      },
      {
          "english": "month",
          "arabic": "شهر"
      },
      {
          "english": "day",
          "arabic": "يوم"
      },
      {
          "english": "week",
          "arabic": "اسبوع"
      },
      {
          "english": "  Follow Up /All",
          "arabic": "متابعة الكل"
      },
      {
          "english": "All",
          "arabic": "الكل"
      },
      {
          "english": "Direction",
          "arabic": "الوجهة"
      }

      
      
      
];