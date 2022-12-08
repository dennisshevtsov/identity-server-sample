import { TaskDto } from './task.dto';

export class GetTasksResponseDto {
  public constructor(
    public tasks: TaskDto[],
    public total: number,
    public pageNo: number,
    public pageCount: number,
  ) {}
}
