import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { IListGroupFactory } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { AppCommandStep } from "../../Lib/AppCommandStep";
import { CommandStepListItemView } from "./CommandStepListItemView";

export class CommandStepListFactory implements IListGroupFactory<CommandStepListItem, CommandStepListItemView> {
    createItem(step: AppCommandStep, itemView: CommandStepListItemView) {
        return new CommandStepListItem(step, itemView);
    }
}

export class CommandStepListItem extends BasicComponent {
    private readonly activityTextComponent: TextComponent;
    private readonly timeStartedTextComponent: TextComponent;
    private readonly timeEndedTextComponent: TextComponent;
    private readonly errorMessageTextComponent: TextComponent;

    constructor(private step: AppCommandStep, protected readonly view: CommandStepListItemView) {
        super(view);
        this.activityTextComponent = this.addComponent(new TextComponent(view.activityTextView));
        this.timeStartedTextComponent = this.addComponent(new TextComponent(view.timeStartedTextView));
        this.timeEndedTextComponent = this.addComponent(new TextComponent(view.timeEndedTextView));
        this.errorMessageTextComponent = this.addComponent(new TextComponent(view.errorMessageTextView));
        this.load(step);
    }

    isMatch(updatedStep: AppCommandStep) {
        return this.step.id === updatedStep.id;
    }

    update(updatedStep: AppCommandStep) {
        this.step = updatedStep;
        this.load(this.step);
    }

    private load(step: AppCommandStep) {
        this.activityTextComponent.setText(step.activity);
        this.timeStartedTextComponent.setText(step.timeStarted.isMaxYear ? "" : step.timeStarted.formatTime());
        this.timeEndedTextComponent.setText(step.timeEnded.isMaxYear ? "..." : step.timeEnded.formatTime());
        this.errorMessageTextComponent.setText(step.errorMessage);
        if (step.errorMessage) {
            this.view.styleAsFailed();
        }
        else if (!step.timeStarted.isMaxYear && step.timeEnded.isMaxYear) {
            this.view.styleAsInProgress();
        }
    }
}