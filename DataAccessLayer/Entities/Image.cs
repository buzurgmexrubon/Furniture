﻿namespace DataAccessLayer.Entities;

public class Image : BaseEntity
{
  [Required, StringLength(500)]
  public string Url { get; set; } = string.Empty;

  public int? FurnitureId { get; set; }

  public Furniture? Furniture { get; set; }

  public int? FeedbackId { get; set; }

  public Feedback? Feedback { get; set; }
}