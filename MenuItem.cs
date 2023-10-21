class MenuItem
{
    private string? _description;
    private string? _name;


    private Func<string>? _action;

    public string? Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string? Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Func<string>? Action
    {
        get { return _action; }
        set { _action = value; }
    }

    public MenuItem()
    {
    }

    public MenuItem(string name, string description, Func<string> action)
    {
        _name = name;
        _description = description;
        _action = action;
    }
}

