namespace WinFormsApp2.DatabaseModel;

public static class AppointmentStatus
{
    /// <summary>已预约：患者已经预约但尚未到达预约时间。</summary>
    public static ulong Scheduled = 1;

    /// <summary>已取消：患者或医生取消了预约。</summary>
    public static ulong Cancelled = 2;

    /// <summary>已完成：预约已经完成，患者已经接受了相应的服务。</summary>
    public static ulong Completed = 3;
}