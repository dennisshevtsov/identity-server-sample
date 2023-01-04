export class GetErrorResponseDto {
  public constructor(
    public readonly errorId: string,
    public readonly message: string,
  ) { }
}
