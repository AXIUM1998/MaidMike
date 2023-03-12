using Microsoft.Extensions.Logging;

namespace MaidMike.Helpers; 

public static class MaidEvents  {
	public static EventId MaiidMike { get; } = new EventId(999, nameof(MaiidMike));
}