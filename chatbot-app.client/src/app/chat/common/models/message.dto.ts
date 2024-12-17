import { MessageTypeEnum } from './message-type.enum';

export interface MessageDto{
  id: number;
  content: string;
  type: MessageTypeEnum;
  createdAt: Date;
  likeDislike?: number;
}

