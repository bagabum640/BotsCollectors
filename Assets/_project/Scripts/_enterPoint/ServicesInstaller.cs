using UnityEngine;

public class ServicesInstaller : MonoBehaviour
{
    [SerializeField] private ConstructionService _constructionService;
    [SerializeField] private Base _mainBase;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private ResourceContainer _resourceContainer;
    [SerializeField] private SelectionIndicator _selectionIndicator;
    [SerializeField] private FlagHandler _flagHandler;
    [SerializeField] private Mover _mover;
    [SerializeField] private Builder _builder;
    [SerializeField] private ResourcePicker _picker;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private ScoreView _scoreView;

    private void Awake()
    {
        ServiceLocator.Register<IConstructionService>(_constructionService);
        ServiceLocator.Register<IBase>(_mainBase);
        ServiceLocator.Register<IScanner>(_scanner);
        ServiceLocator.Register<IResourceCounter>(_resourceCounter);
        ServiceLocator.Register<IResourceContainer>(_resourceContainer);
        ServiceLocator.Register<ISelectionIndicator>(_selectionIndicator);
        ServiceLocator.Register<IFlagHandler>(_flagHandler);
        ServiceLocator.Register<IMover>(_mover);
        ServiceLocator.Register<IBuilder>(_builder);
        ServiceLocator.Register<IResourcePicker>(_picker);
        ServiceLocator.Register<IUnitSpawner>(_unitSpawner);
        ServiceLocator.Register<IBaseSpawner>(_baseSpawner);
        ServiceLocator.Register<IScoreView>(_scoreView);
    }
}