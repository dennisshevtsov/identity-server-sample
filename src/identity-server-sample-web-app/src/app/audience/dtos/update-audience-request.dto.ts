export class UpdateAudienceRequestDto {
  public constructor(
    public readonly audienceName: string,
    public readonly displayName : string,
    public readonly description : string,
    public readonly scopes      : string[],
  ) {}
}
