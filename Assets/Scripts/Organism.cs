using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;

public class Organism : MonoBehaviour
{
    public float size { get; private set; }
    public float startSize;

    public float maxSize;
    public float minSize;
    public float movementSpeed;

    public float nutrientAbsorptionRate;
    public float phagosisAbsorptionRate;

    public float nutrientEfficiency = 1;
    public float phagosisEfficiency = 1;

    public float phagosisSizeFactor = 0.5f;

    public float baseMovementCost = 1;

    public int reproductionNumber;

    public float mutationRate;

    public Color color;

    public Vector2 position { get { return rb.position; } }

    public List<Mutation> mutations = new List<Mutation>();

    public Action onIdle;
    public Action onSizeMax;
    public Action onSizeMin;

    public Action<Nutrient> onNutrientContact;
    public Action<Organism> onOrganismContact;

    public Action<Organism> onOrganismSense;
    public Action<Nutrient> onNutrientSense;

    public Action reproductionDefault;
    public Action deathDefault;
    public Action moveDefault;
    public Action<Nutrient> eatDefault;
    public Action mutateDefault;


    public bool canEat { get; private set; } = true;
    public bool canReproduce { get; private set; } = true;

    //-----------

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 targetLocation;
    public const float baseScale = 25;

    private List<Mutation> eatRestrictions = new List<Mutation>();
    

    //------------
    //debug

    public bool isBlocked;
    public Vector2 debug_direction;
    public Vector2 debug_direction_target;

    //-----


    public void SetSize(float value)
    {
        if (value >= maxSize)
        {
            size = value;
            onSizeMax?.Invoke();
        }
        else if (value <= minSize)
        {
            if (value < 0) size = 0;
            else size = value;
            onSizeMin?.Invoke();
        }
        else size = value;
    }

    public void AddSize(float value)
    {
        SetSize(size + value);
    }

    public void SetDirection(Vector2 value)
    {
        targetLocation = rb.position + value.normalized * movementSpeed;
    }

    public void Kill()
    {
        deathDefault();
    }

    public void AddEatRestriction(Mutation m)
    {
        canEat = false;
        eatRestrictions.Add(m);
    }

    public void RemoveEatRestriction(Mutation m)
    {
        eatRestrictions.Remove(m);
        if (eatRestrictions.Count == 0)
            canEat = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {     
        if (moveDefault == null)
            AddMutation("MoveBasic");
        if (eatDefault == null)
            AddMutation("EatBasic");
        if (reproductionDefault == null)
            AddMutation("ReproduceBasic");
        if (deathDefault == null)
            AddMutation("DieBasic");
        if (mutateDefault == null)
            AddMutation("MutateBasic");

        onIdle += moveDefault;
        onNutrientContact += eatDefault;
        onSizeMax += reproductionDefault;
        onSizeMin += deathDefault;

        mutateDefault();

        spriteRenderer.color = color;

        if (startSize > maxSize)
        {
            Debug.LogError("Error, startSize < maxSize");
        }

        if (size == 0)
            SetSize(startSize);
        else SetSize(size);
    }

    private void SetUpMutations()
    {
        //sort mutations by priority so that the Setup of higher priority 
        //(0 is highest priority, descending order) is called first
        mutations.Sort((Mutation a, Mutation b) => a.priority.CompareTo(b.priority));

        foreach(Mutation mutation in mutations)
        {
            mutation.AddToOrganism(this);
        }
    }

    public void AddMutation(string mutationName)
    {
        Mutation m = MutationResources.Get(mutationName);
        if (m == null) return;

        m.AddToOrganism(this);
        mutations.Add(m);
    }

    public void AddMutation()
    {
        Mutation m = MutationResources.GetRandom();
        Mutation existing = mutations.Find(x => x.GetType() == m.GetType());
        if (existing != null) return;

        m.AddToOrganism(this);
        mutations.Add(m);
    }

    private void FixedUpdate()
    {
        onIdle?.Invoke();

        Vector2 direction = (targetLocation - rb.position).normalized;
        Vector2 point = rb.position + direction * movementSpeed * Time.fixedDeltaTime;
        //if (Boundaries.Contains(point))
        //{
        //    rb.MovePosition(point);
        //}

        rb.MovePosition(point);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Organism")
        {
            Organism other = collision.gameObject.GetComponent<Organism>();
            onOrganismContact?.Invoke(other);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nutrient")
        {
            Nutrient other = collision.gameObject.GetComponent<Nutrient>();
            onNutrientContact?.Invoke(other);
        }
    }

    private void ScaleTween(float targetVal)
    {
        //magic number for now
        transform.DOScale(targetVal, 0.4f);
    }

    private void Update()
    {
        if(Mathf.Abs(transform.localScale.x - size/baseScale) > 0.1f)
            ScaleTween(size / baseScale);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        mutations.Clear();


    }

}
