using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LessonMvc.Models;
public class Book
{
    public Book()
    {
        //Category category =  new Category();
    }        
    public int Id { get; set; }
    [DisplayName("Başlık")]
    public string? Baslik { get; set; }
    public string? Aciklama { get; set; }
    public string? Tur { get; set; }
    
    [DataType(DataType.Date, ErrorMessage = "Date only")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime BasimTarih { get; set; }
    public double Fiyat { get; set; }

    [DisplayName("Stok Adedi")]
    public int StokAdet { get; set; }

    public bool Indirimli { get; set; }

    //public Category category { get; set; }
}

