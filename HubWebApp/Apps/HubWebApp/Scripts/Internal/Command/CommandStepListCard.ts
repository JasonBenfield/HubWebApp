import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { AppCommandStep } from "../../Lib/AppCommandStep";
import { CommandStepListCardView } from "./CommandStepListCardView";
import { CommandStepListFactory, CommandStepListItem } from "./CommandStepListItem";
import { CommandStepListItemView } from "./CommandStepListItemView";

export class CommandStepListCard extends BasicComponent {
    private readonly stepListGroup: ListGroup<CommandStepListItem, CommandStepListItemView>;
    private readonly stepListFactory = new CommandStepListFactory();

    constructor(view: CommandStepListCardView) {
        super(view);
        this.stepListGroup = this.addComponent(
            new ListGroup(view.stepListView, this.stepListFactory)
        );
    }

    clear() {
        this.stepListGroup.clearItems();
    }

    addOrUpdateCommandSteps(steps: AppCommandStep[]) {
        for (const step of steps) {
            const listItem = this.stepListGroup.getItems().find(li => li.isMatch(step));
            if (listItem) {
                listItem.update(step);
            }
            else {
                this.stepListGroup.addItem(step, this.stepListFactory.createItem);
            }
        }
    }

    show() { this.view.show(); }

    hide() { this.view.hide(); }
}