export class GetScopeResponseDto {
  public constructor(
    public readonly scopeName  : string,
    public readonly displayName: string,
  ) {}
}
