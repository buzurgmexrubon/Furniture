﻿namespace DataAccessLayer.Entities;

public class FeedbackBan : BaseEntity
{
  public int FeedbackId { get; set; }

  //public Feedback Feedback { get; set; } = null!;
  public Feedback Feedback { get; set; } = new();

  //public string UserId { get; set; } = null!;
  public string UserId { get; set; } = string.Empty;

  //public User User { get; set; } = null!;
  public User User { get; set; } = new();
}