namespace ECommerce.Shared;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal? Price { get; set; } = 0;
    public int CategoryId { get; set; }
    public bool Featured { get; set; } = false;
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    public bool Editing { get; set; } = false;
    public bool IsNew { get; set; } = false;
}