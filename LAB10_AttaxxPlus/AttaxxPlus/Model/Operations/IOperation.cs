namespace AttaxxPlus.Model.Operations
{
    // EVIP: unified interface for all operations in the game
    public interface IOperation
    {
		/// <summary>
		/// Tries to execute this operation
		/// </summary>
		/// <param name="selectedField">A field previously selected by the player (may be null if there is none)</param>
		/// <param name="currentField">The field currently clicked</param>
		/// <returns>True if executed. False if cannot execute (no side effects)</returns>
		bool TryExecute(Field selectedField, Field currentField);

		/// <summary>
		/// Returns a score for this operation. Score is the number of squares acquired due to the move. Returns -1 if the move cannot be performed.
		/// </summary>
		/// <param name="selectedField">A field previously selected by the player (may be null if there is none)</param>
		/// <param name="currentField">The field currently clicked</param>
		/// <returns>Returns a score for this operation. -1 if cannot execute (no side effects)</returns>
		int GetExecutionScore(Field selectedField, Field currentField);
	}
}
