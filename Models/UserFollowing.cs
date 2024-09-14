using SocailMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class UserFollower
{
    public int FollowerId { get; set; }
    public User Follower { get; set; }

    public int FollowedId { get; set; }
    public User Followed { get; set; }
}